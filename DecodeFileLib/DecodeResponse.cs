using System;

namespace DecodeFileLib
{
    class DecodeResponse
    {
        private String key;
        private String fileName;
        private String secret;

        public String Key
        {
            get { return this.key; }
            set { this.key = value; }
        }

        public String FileName
        {
            get { return this.fileName; }
            set { this.fileName = value; }
        }

        public String Secret
        {
            get { return this.secret; }
            set { this.secret = value; }
        }

    }
}
