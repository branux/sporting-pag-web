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
using System.Drawing.Drawing2D;

namespace SportingWeb.Admin
{
    public partial class Equipo_consola : System.Web.UI.Page
    {
        private static String sessionVar_imageToSaveInDB = "imgJugPathsToSaveInBD";
        Imagen imageToSaveInDB; //guardo los path (Imagen chica y grande) para guardarlos en BD.

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                cargarJugadores();
                limpiarCampos();
            }
            else
            {
                setSuccessColorOutput(false);
            }
        }

        private void cargarJugadores()
        {
            try
            {
                DataTable dtJugadores = new DataTable();
                dtJugadores.Columns.Add("idJugador");
                dtJugadores.Columns.Add("nombreApellido");
                dtJugadores.Columns.Add("posicion");
                dtJugadores.Columns.Add("imagen");

                foreach (Jugador jugador in GestorPlantel.getJugadoresPlantelActual())
                {
                    DataRow row = dtJugadores.NewRow();
                    row["idJugador"] = jugador.IdJugador;
                    row["nombreApellido"] = jugador.NombreApellido;
                    row["posicion"] = jugador.Posicion;
                    row["imagen"] = jugador.Foto.PathSmall;

                    dtJugadores.Rows.Add(row);
                }

                grillaJugadores.DataSource = dtJugadores;
                grillaJugadores.DataBind();
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
            sSavePath = "images/plantel/actual/";
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
                        imgJugador.ImageUrl = null;
                        return;
                    }

                    // Check file extension (must be JPG)
                    if (System.IO.Path.GetExtension(myFile.FileName).ToLower() != ".jpg")
                    {
                        setSuccessColorOutput(false);
                        lblOutput.Text = "Error: La imagen debe tener una extension JPG.";
                        imgJugador.ImageUrl = null;
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

                        // Save thumbnail and output it onto the webpage
                        System.Drawing.Image myThumbnail
                                = myBitmap.GetThumbnailImage(intThumbWidth,
                                                             intThumbHeight, myCallBack, IntPtr.Zero);
                        myThumbnail.Save(Server.MapPath(sSavePath + sThumbFile));
                        imgJugador.ImageUrl = sSavePath + sThumbFile;

                        //Agrego los path de la imagen para registrarlos en la BD
                        imageToSaveInDB = new Imagen();
                        if (Session[sessionVar_imageToSaveInDB] != null)
                        {
                            imageToSaveInDB = (Imagen)Session[sessionVar_imageToSaveInDB];
                        }
                        imageToSaveInDB.PathBig = sSavePath + sFilename;
                        imageToSaveInDB.PathSmall = sSavePath + sThumbFile;
                        Session[sessionVar_imageToSaveInDB] = imageToSaveInDB;

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
            txtNomApe.Text = "";
            txtPosicion.Text = "";
            imgJugador.ImageUrl = null;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                Jugador jugador = new Jugador();
                try
                {
                    jugador.NombreApellido = txtNomApe.Text;
                    jugador.Posicion = txtPosicion.Text;
                    imageToSaveInDB = (Imagen)Session[sessionVar_imageToSaveInDB];
                    jugador.Foto = imageToSaveInDB;

                    if (txtId.Text.CompareTo("") != 0)
                    {
                        //Modifico un jugador existente
                        jugador.IdJugador = Convert.ToInt32(txtId.Text);
                        GestorPlantel.updateJugador_plantelActual(jugador);
                        setSuccessColorOutput(true);
                        lblOutput.Text = "Jugador actualizado con éxito!";
                    }
                    else
                    {
                        //Guardo el nuevo jugador
                        GestorPlantel.registrarJugador_plantelActual(jugador);
                        setSuccessColorOutput(true);
                        lblOutput.Text = "Jugador registrado con éxito!";
                    }
                    /* No limpio los paths de las imagenes en el limpiarCampos() porque el limpiarCampos()
                     * se ejecuta en el load y la variable session no se tiene que limpiar ahi.*/
                    Session[sessionVar_imageToSaveInDB] = new Imagen();
                    cargarJugadores();
                    limpiarCampos();
                    grillaJugadores.SelectedIndex = -1;
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
                lblOutput.Text = "Error al registrar el jugador.";
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            txtNomApe.Focus();
            lblOutput.Text = "";
            Session[sessionVar_imageToSaveInDB] = new Imagen();
            grillaJugadores.SelectedIndex = -1;
            disableEditableElements(false);
        }

        private void disableEditableElements(bool disable)
        {
            //si disable == true deshabilito los elementos
            if (disable)
            {
                txtNomApe.Enabled = false;
                txtPosicion.Enabled = false;
                btnCargarImagen.Enabled = false;
                btnGuardar.Enabled = false;
            }
            else
            {
                //disable = false, entonces habilito los elementos
                txtNomApe.Enabled = true;
                txtPosicion.Enabled = true;
                btnCargarImagen.Enabled = true;
                btnGuardar.Enabled = true;
            }
        }

        protected void GrillaJugadores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                //obtengo el id del jugador a ser Modificado o Eliminado
                int id = Convert.ToInt32(e.CommandArgument.ToString());
                if (e.CommandName == "Eliminar")
                {
                    //Obtengo la foto del jugador antes de borrarlo de la BD
                    Imagen fotoJugador = GestorPlantel.getJugador_plantelActual(id).Foto;
                    GestorPlantel.deleteJugador_plantelActual(id);
                    //luego que borre el accesorio de la BD tengo que borrar las imagenes que estan en el server
                    eliminarImagenesDelServer(fotoJugador);

                    setSuccessColorOutput(true);
                    lblOutput.Text = "El jugador fue eliminado con exito";

                    cargarJugadores();
                    limpiarCampos();
                    Session[sessionVar_imageToSaveInDB] = new Imagen();
                    grillaJugadores.SelectedIndex = -1;
                    /* habilito todos los elementos editables */
                    disableEditableElements(false);
                    txtNomApe.Focus();
                }
                if (e.CommandName == "Editar")
                {
                    limpiarCampos();
                    lblOutput.Text = "";
                    //traigo los datos del jugador de la bd
                    Jugador jugador = GestorPlantel.getJugador_plantelActual(id);
                    //cargo los datos del jugador
                    txtId.Text = jugador.IdJugador.ToString();
                    txtNomApe.Text = jugador.NombreApellido;
                    txtPosicion.Text = jugador.Posicion;
                    
                    //pongo la foto del jugador en la variable session
                    Imagen fotoJugador = jugador.Foto;

                    if (fotoJugador != null)
                    {
                        Session[sessionVar_imageToSaveInDB] = fotoJugador;
                    }
                    else
                    {
                        lblOutput.Text = "Error al intentar obtener la foto del jugador";
                        return;
                    }
                    //cargo la foto del jugador
                    imgJugador.ImageUrl = fotoJugador.PathSmall;
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
            try
            {
                if (imagen.PathBig.Equals("") == false && imagen.PathBig.Length > 0)
                {
                    System.IO.File.Delete(Server.MapPath(imagen.PathBig));
                }
                if (imagen.PathMedium.Equals("") == false && imagen.PathMedium.Length > 0)
                {
                    System.IO.File.Delete(Server.MapPath(imagen.PathMedium));
                }
                if (imagen.PathSmall.Equals("") == false && imagen.PathSmall.Length > 0)
                {
                    System.IO.File.Delete(Server.MapPath(imagen.PathSmall));
                }
            }
            catch (Exception e)
            {
                setSuccessColorOutput(false);
                lblOutput.Text = "Error al borrar las imagenes del jugador en el servidor. " + e.Message;
            }
        }
    }
}
