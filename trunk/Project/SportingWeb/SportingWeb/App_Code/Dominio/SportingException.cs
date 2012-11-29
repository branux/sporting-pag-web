using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportingWeb
{
    public class SportingException : Exception
    {
        private String message;

        public SportingException(String message)
	    {
            this.Message = message;
	    }
        
    
        public String Message
        {
            get { return this.message; }
            set { this.message = value; }
        }
    }
}