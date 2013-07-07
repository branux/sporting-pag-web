using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Drawing;
using System.Collections.Generic;
using System.Drawing.Drawing2D;

namespace SportingWeb.Admin
{
    public partial class Noticias_consola : System.Web.UI.Page
    {
        List<Imagen> imagesToSaveInDB;//lista de las imagenes para registrar en BD
        private static String sessionVar_imagesToSaveInDB = "imgNoticiasPathsToSaveInBD";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                if (!Page.IsPostBack)
                {
                    currentPage.Value = "Noticias";
                    cargarNoticias();
                    limpiarCampos();
                }
                else
                {
                    setSuccessColorOutput(false);
                }
            }
        }

        private void cargarNoticias()
        {
            try
            {
                DataTable dtNoticias = new DataTable();
                dtNoticias.Columns.Add("idNoticia");
                dtNoticias.Columns.Add("titulo");
                dtNoticias.Columns.Add("desc");
                dtNoticias.Columns.Add("principal");
                dtNoticias.Columns.Add("imagenes");

                foreach (Noticia noticia in GestorNoticias.getNoticias())
                {
                    DataRow row = dtNoticias.NewRow();
                    row["idNoticia"] = noticia.IdNoticia;
                    row["titulo"] = noticia.Titulo;
                    row["desc"] = noticia.Descripcion;
                    row["principal"] = noticia.Principal;
                    row["imagenes"] = noticia.Imagenes[0].PathSmall;

                    dtNoticias.Rows.Add(row);
                }

                grillaNoticias.DataSource = dtNoticias;
                grillaNoticias.DataBind();
            }
            catch (Exception er)
            {
                setSuccessColorOutput(false);
                lblOutput.Text = er.Message;
            }
        }

        protected void btnCargarImagen_Click(object sender, EventArgs e)
        {
            // Initialize variables
            string sSavePath;
            string sThumbExtension;
            int intThumbWidth;
            int intThumbHeight;

            // Set constant values
            sSavePath = "images/noticias/";
            sThumbExtension = "_thumb";
            intThumbWidth = 120;
            intThumbHeight = 100;

            // If file field is not empty
            if (fileUpload.PostedFile != null)
            {
                try
                {
                    // Check file size (must not be 0)
                    HttpPostedFile myFile = fileUpload.PostedFile;
                    int nFileLen = myFile.ContentLength;
                    if (nFileLen == 0)
                    {
                        setSuccessColorOutput(false);
                        lblOutput.Text = "Ninguna imagen fue cargada.";
                        return;
                    }

                    // Check file extension (must be JPG)
                    if (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".jpg")
                    {
                        setSuccessColorOutput(false);
                        lblOutput.Text = "Error: La imagen debe tener una extension JPG.";
                        return;
                    }

                    // Read file into a data stream
                    byte[] myData = new Byte[nFileLen];
                    myFile.InputStream.Read(myData, 0, nFileLen);

                    // Make sure a duplicate file does not exist.  If it does, keep on appending an
                    // incremental numeric until it is unique
                    string sFilename = System.IO.Path.GetFileName(myFile.FileName);
                    int file_append = 0;
                    while (System.IO.File.Exists(Server.MapPath(sSavePath + sFilename)))
                    {
                        file_append++;
                        sFilename = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName)
                                         + file_append.ToString() + ".jpg";
                    }

                    sSavePath = "../" + sSavePath;
                    try
                    {
                        // Changing the image size before saving image to disk
                        using (System.Drawing.Image image = System.Drawing.Image.FromStream(myFile.InputStream))
                        {
                            // can given width of image as we want
                            double scaleFactor = 0.5;
                            int newWidth = (int)(image.Width * scaleFactor);
                            // can given height of image as we want
                            int newHeight = (int)(image.Height * scaleFactor);
                            Bitmap thumbnailImg = new Bitmap(newWidth, newHeight);
                            Graphics thumbGraph = Graphics.FromImage(thumbnailImg);
                            thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                            thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                            thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            Rectangle imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                            thumbGraph.DrawImage(image, imageRectangle);
                            // Save the image to disk
                            thumbnailImg.Save(Server.MapPath(sSavePath + sFilename), image.RawFormat);

                            // Closing resources
                            thumbGraph.Dispose();
                            thumbnailImg.Dispose();
                            image.Dispose();
                        }
                    }
                    catch (Exception exe)
                    {
                        setSuccessColorOutput(false);
                        lblOutput.Text = lblOutput.Text + "\nPath: " + sSavePath + "\n Error del sistema: " + exe.Message;
                    }

                    // Check whether the file is really a JPEG by opening it
                    System.Drawing.Image.GetThumbnailImageAbort myCallBack =
                                   new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);
                    Bitmap myBitmap;
                    try
                    {
                        myBitmap = new Bitmap(Server.MapPath(sSavePath + sFilename));

                        // If jpg file is a jpeg, create a thumbnail filename that is unique.
                        file_append = 0;
                        string sThumbFile = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName)
                                                                 + sThumbExtension + ".jpg";
                        while (System.IO.File.Exists(Server.MapPath(sSavePath + sThumbFile)))
                        {
                            file_append++;
                            sThumbFile = System.IO.Path.GetFileNameWithoutExtension(myFile.FileName) +
                                           file_append.ToString() + sThumbExtension + ".jpg";
                        }

                        // Save thumbnail
                        System.Drawing.Image myThumbnail
                                = myBitmap.GetThumbnailImage(intThumbWidth,
                                                             intThumbHeight, myCallBack, IntPtr.Zero);
                        myThumbnail.Save(Server.MapPath(sSavePath + sThumbFile));

                        //Agrego los path de la imagen para registrarlos en la BD
                        imagesToSaveInDB = new List<Imagen>();
                        Imagen img = new Imagen();
                        if (Session[sessionVar_imagesToSaveInDB] != null)
                        {
                            imagesToSaveInDB = (List<Imagen>)Session[sessionVar_imagesToSaveInDB];
                        }
                        img.PathBig = sSavePath + sFilename;
                        img.PathSmall = sSavePath + sThumbFile;
                        imagesToSaveInDB.Add(img);
                        Session[sessionVar_imagesToSaveInDB] = imagesToSaveInDB;

                        //Agrego la imagen a la grilla de imagenes
                        cargarImagenesNoticia_enGrilla(imagesToSaveInDB);

                        // Displaying success information
                        setSuccessColorOutput(true);
                        lblOutput.Text = "Imagen cargada con éxito!";

                        // Destroy objects
                        myThumbnail.Dispose();
                        myBitmap.Dispose();
                    }
                    catch (ArgumentException errArgument)
                    {
                        // The file wasn't a valid jpg file
                        setSuccessColorOutput(false);
                        lblOutput.Text = lblOutput.Text + "\nEl archivo no es una imagen valida. Detalle: " + errArgument.Message;
                        System.IO.File.Delete(Server.MapPath(sSavePath + sFilename));
                    }
                }
                catch (Exception ex)
                {
                    setSuccessColorOutput(false);
                    lblOutput.Text = "Error del sistema: " + ex.Message;
                }
            }
        }

        private void setSuccessColorOutput(bool isSuccess)
        {
            if (isSuccess)
            {
                lblOutput.ForeColor = Color.Green;
            }
            else
            {
                lblOutput.ForeColor = Color.Red;
            }
        }

        public bool ThumbnailCallback()
        {
            return false;
        }

        private void limpiarCampos()
        {
            txtIdNoticia.Text = "";
            txtTitulo.Text = "";
            txtDesc.Text = "";
            grillaImagenes.DataSource = null;
            grillaImagenes.DataBind();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Noticia noticia = new Noticia();
                try
                {
                    noticia.Titulo = txtTitulo.Text;
                    noticia.Descripcion = txtDesc.Text;
                    imagesToSaveInDB = (List<Imagen>)Session[sessionVar_imagesToSaveInDB];
                    noticia.Imagenes = imagesToSaveInDB;

                    if (txtIdNoticia.Text.CompareTo("") != 0)
                    {
                        //Modifico una noticia existente
                        noticia.IdNoticia = Convert.ToInt32(txtIdNoticia.Text);
                        GestorNoticias.updateNoticia(noticia);
                        setSuccessColorOutput(true);
                        lblOutput.Text = "Noticia actualizada con éxito!";
                    }
                    else
                    {
                        //Guardo la nueva noticia
                        GestorNoticias.registrarNoticia(noticia);
                        setSuccessColorOutput(true);
                        lblOutput.Text = "Noticia registrada con éxito!";
                    }
                    /* No limpio los paths de las imagenes en el limpiarCampos() porque el limpiarCampos()
                     * se ejecuta en el load y la variable session no se tiene que limpiar ahi.*/
                    Session[sessionVar_imagesToSaveInDB] = new List<Imagen>();
                    cargarNoticias();
                    limpiarCampos();
                    grillaNoticias.SelectedIndex = -1;
                }
                catch (PathImgEmptyException imgEx)
                {
                    setSuccessColorOutput(false);
                    lblOutput.Text = imgEx.Message;
                }
                catch (SportingException spEx)
                {
                    setSuccessColorOutput(false);
                    lblOutput.Text = spEx.Message;
                }
                catch (Exception ex)
                {
                    setSuccessColorOutput(false);
                    lblOutput.Text = ex.Message;
                }
            }
            else
            {
                lblOutput.Text = "Error al registrar la noticia.";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            txtTitulo.Focus();
            lblOutput.Text = "";
            Session[sessionVar_imagesToSaveInDB] = new List<Imagen>();
            grillaNoticias.SelectedIndex = -1;
            disableEditableElements(false);
        }

        private void disableEditableElements(bool disable)
        {
            //si disable == true deshabilito los elementos
            if (disable)
            {
                txtDesc.Enabled = false;
                txtTitulo.Enabled = false;
                btnCargarImagen.Enabled = false;
                btnGuardar.Enabled = false;
            }
            else
            {
                //disable = false, entonces habilito los elementos
                txtDesc.Enabled = true;
                txtTitulo.Enabled = true;
                btnCargarImagen.Enabled = true;
                btnGuardar.Enabled = true;
            }
        }

        protected void GrillaNoticias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //obtengo el id de la noticia a ser Modificada o Eliminada
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName == "Eliminar")
                {
                    //Obtengo la foto del jugador antes de borrarlo de la BD
                    List<Imagen> imagenesNoticia = GestorNoticias.getNoticia(id).Imagenes;
                    GestorNoticias.deleteNoticia(id);
                    //luego que borre la noticia de la BD tengo que borrar las imagenes que estan en el server
                    eliminarImagenesDelServer(imagenesNoticia);

                    setSuccessColorOutput(true);
                    lblOutput.Text = "La noticia fue eliminada con éxito";

                    cargarNoticias();
                    limpiarCampos();
                    Session[sessionVar_imagesToSaveInDB] = new List<Imagen>();
                    grillaNoticias.SelectedIndex = -1;
                    /* habilito todos los elementos editables */
                    disableEditableElements(false);
                    txtTitulo.Focus();
                }
                if (e.CommandName == "Editar")
                {
                    limpiarCampos();
                    lblOutput.Text = "";
                    //traigo los datos de la noticia de la bd
                    Noticia noticia = GestorNoticias.getNoticia(id);
                    //cargo los datos de la noticia
                    txtIdNoticia.Text = noticia.IdNoticia.ToString();
                    txtTitulo.Text = noticia.Titulo;
                    txtDesc.Text = noticia.Descripcion;

                    //pongo las imagenes de la noticia en la variable session
                    List<Imagen> imagenesNoticia = noticia.Imagenes;

                    if (imagenesNoticia != null)
                    {
                        Session[sessionVar_imagesToSaveInDB] = imagenesNoticia;
                    }
                    else
                    {
                        lblOutput.Text = "Error al intentar obtener las imagenes de la noticia";
                        return;
                    }
                    //cargo las imagenes de la noticia
                    cargarImagenesNoticia_enGrilla(imagenesNoticia);
                }
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutput.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutput.Text = ex.Message;
            }
        }

        protected void deleteImg_grillaImagenes_Click(object sender, EventArgs e)
        {
            try
            {
                //Obtengo la fila que se le hizo click
                GridViewRow clickedRow = ((ImageButton)sender).NamingContainer as GridViewRow;
                
                //Recupero la imagen thumb que se esta mostrando en la grilla
                System.Web.UI.WebControls.Image img = clickedRow.Cells[1].Controls[0] as System.Web.UI.WebControls.Image;
                if (img != null)
		        {
                    /* Recorro todas las imagenes de Session[sessionVar_imagesToSaveInDB]
                     * para encontrar todas las url de la imagen y poder borrarla de:
                     * 1- el server y 2- la var Session[sessionVar_imagesToSaveInDB] */
                    imagesToSaveInDB = (List<Imagen>)Session[sessionVar_imagesToSaveInDB];
                    if (imagesToSaveInDB != null)
                    {
                        foreach (Imagen imagen in imagesToSaveInDB)
                        {
                            if (img.ImageUrl.Equals(imagen.PathSmall))
                            {
                                /* Imagen a borrar encontrada!!! */
                                eliminarImagenesDelServer(imagen);

                                imagesToSaveInDB.Remove(imagen);
                                Session[sessionVar_imagesToSaveInDB] = imagesToSaveInDB;

                                cargarImagenesNoticia_enGrilla(imagesToSaveInDB);

                                setSuccessColorOutput(true);
                                lblOutput.Text = "La imagen fue eliminada con éxito";
                                
                                return;
                            }
                        }
                    }
		        }
            }
            catch (SportingException spEx)
            {
                setSuccessColorOutput(false);
                lblOutput.Text = spEx.Message;
            }
            catch (Exception ex)
            {
                setSuccessColorOutput(false);
                lblOutput.Text = ex.Message;
            }
        }


        private void eliminarImagenesDelServer(Imagen imagen)
        {
            List<Imagen> imagenes = new List<Imagen>();
            imagenes.Add(imagen);

            eliminarImagenesDelServer(imagenes);
        }

        private void eliminarImagenesDelServer(List<Imagen> imagenes)
        {
            try
            {
                foreach (Imagen imagen in imagenes)
                {
                    if (imagen != null)
	                {
                        String pathBigh = imagen.PathBig;
                        String pathMedium = imagen.PathMedium;
                        String pathSmall = imagen.PathSmall;

                        if (pathBigh != null && !pathBigh.Equals("") && pathBigh.Length > 0)
                        {
                            System.IO.File.Delete(Server.MapPath(pathBigh));
                        }
                        if (pathMedium != null && !pathMedium.Equals("") && pathMedium.Length > 0)
                        {
                            System.IO.File.Delete(Server.MapPath(pathMedium));
                        }
                        if (pathSmall != null && !pathSmall.Equals("") && pathSmall.Length > 0)
                        {
                            System.IO.File.Delete(Server.MapPath(pathSmall));
                        }
	                }
                }
            }
            catch (Exception e)
            {
                setSuccessColorOutput(false);
                lblOutput.Text = "Error al borrar las imagenes de la noticia en el servidor. " + e.Message;
            }
        }

        private void cargarImagenesNoticia_enGrilla(List<Imagen> imagenes)
        {
            //Agrego las imagenes a la grilla grillaImagenes
            DataTable dtImagenes = new DataTable();
            dtImagenes.Columns.Add("idImagen");
            dtImagenes.Columns.Add("image");

            DataRow rowImage;
            foreach (Imagen img in imagenes)
            {
                rowImage = dtImagenes.NewRow();
                rowImage["idImagen"] = img.IdImagen.ToString();
                rowImage["image"] = img.PathSmall;
                dtImagenes.Rows.Add(rowImage);
            }
            grillaImagenes.DataSource = dtImagenes;
            grillaImagenes.DataBind();
        }
    }
}
