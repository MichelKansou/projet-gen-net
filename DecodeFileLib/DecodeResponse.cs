using System;

namespace DecodeFileLib
{
    public class DecodeResponse
    {
        private String key;
        private String fileName;
        private String secret;
        private String text;
        private float ratio;

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

        public String Text
        {
            get { return this.text; }
            set { this.text = value; }
        }
        public float Ratio
        {
            get { return this.ratio; }
            set { this.ratio = value; }
        }
    }
}
