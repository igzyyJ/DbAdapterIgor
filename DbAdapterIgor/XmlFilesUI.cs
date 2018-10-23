using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAdapterIgor
{
   public class XmlFilesUI
    {
        //source
        private string proizvodiPath;
        private string cijenePath;

        //update file
        private string updateProizvod;

        public string ProizvodiPath {
             get{ return proizvodiPath; }

             set {proizvodiPath = value; }
        }


        public string CijenePath {
            get { return cijenePath; }
            set { cijenePath = value; }
        }

        public string UpdateProizvod {
            get { return updateProizvod; }
            set { updateProizvod = value; }
        }
    }
}
