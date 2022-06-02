using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clsLogica
{
    public class clsEncriptacion
    {
        #region Costructor

        public clsEncriptacion()
        {
        }

        #endregion

        #region Public Method

        /// <summary>
        /// Encript InClear Text using RC4 method using EncriptionKey
        /// Put the result into CryptedText 
        /// </summary>
        /// <returns>true if success, else false</returns>
        public bool mtdEncrypt()
        {
            #region Variables
            // toRet is used to store function retcode
            bool booToRet = true;
            // indexes used below
            long i = 0;
            long j = 0;
            // Put input string in temporary byte array
            Encoding encTexto = Encoding.Default;
            #endregion Variables

            try
            {
                byte[] bytInput = encTexto.GetBytes(this.strM_sInClearText);

                // Output byte array
                byte[] bytOutput = new byte[bytInput.Length];

                // Local copy of m_nBoxLen
                byte[] n_LocBox = new byte[lonM_nLength];
                this.bytM_nBox.CopyTo(n_LocBox, 0);

                //	Len of Chipher
                long ChipherLen = bytInput.Length + 1;

                // Run Alghoritm
                #region Algoritmo
                for (long offset = 0; offset < bytInput.Length; offset++)
                {
                    i = (i + 1) % lonM_nLength;
                    j = (j + n_LocBox[i]) % lonM_nLength;
                    byte temp = n_LocBox[i];
                    n_LocBox[i] = n_LocBox[j];
                    n_LocBox[j] = temp;
                    byte a = bytInput[offset];
                    byte b = n_LocBox[(n_LocBox[i] + n_LocBox[j]) % lonM_nLength];
                    bytOutput[offset] = (byte)((int)a ^ (int)b);
                }
                #endregion

                // Put result into output string ( CryptedText )
                char[] outarrchar = new char[encTexto.GetCharCount(bytOutput, 0, bytOutput.Length)];
                encTexto.GetChars(bytOutput, 0, bytOutput.Length, outarrchar, 0);
                this.strM_sCryptedText = new string(outarrchar);
            }
            catch
            {
                // error occured - set retcode to false.
                booToRet = false;
            }

            // return retcode
            return (booToRet);
        }

        /// <summary>
        /// Decript CryptedText into InClearText using EncriptionKey
        /// </summary>
        /// <returns>true if success else false</returns>
        public bool mtdDecrypt()
        {
            #region Variables
            // toRet is used to store function retcode
            bool booToRet = true;
            #endregion

            try
            {
                this.strM_sInClearText = this.strM_sCryptedText;
                strM_sCryptedText = string.Empty;

                if (booToRet = mtdEncrypt())
                    strM_sInClearText = strM_sCryptedText;
            }
            catch
            {
                // error occured - set retcode to false.
                booToRet = false;
            }

            // return retcode
            return booToRet;
        }

        #endregion

        #region Prop definitions
        /// <summary>
        /// Get or set Encryption Key
        /// </summary>
        public string EncryptionKey
        {
            get
            {
                return (this.strM_sEncryptionKey);
            }
            set
            {
                // assign value only if it is a new value
                if (this.strM_sEncryptionKey != value)
                {
                    this.strM_sEncryptionKey = value;

                    // Used to populate m_nBox
                    long index2 = 0;

                    // Create two different encoding 
                    Encoding ascii = Encoding.ASCII;
                    Encoding unicode = Encoding.Unicode;

                    // Perform the conversion of the encryption key from unicode to ansi
                    byte[] asciiBytes = Encoding.Convert(unicode, ascii, unicode.GetBytes(this.strM_sEncryptionKey));

                    // Convert the new byte[] into a char[] and then to string
                    char[] asciiChars = new char[ascii.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
                    ascii.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
                    this.strM_sEncryptionKeyAscii = new string(asciiChars);

                    // Populate m_nBox
                    long KeyLen = strM_sEncryptionKey.Length;

                    // First Loop
                    for (long count = 0; count < lonM_nLength; count++)
                    {
                        this.bytM_nBox[count] = (byte)count;
                    }

                    // Second Loop
                    for (long count = 0; count < lonM_nLength; count++)
                    {
                        index2 = (index2 + bytM_nBox[count] + asciiChars[count % KeyLen]) % lonM_nLength;
                        byte temp = bytM_nBox[count];
                        bytM_nBox[count] = bytM_nBox[index2];
                        bytM_nBox[index2] = temp;
                    }
                }
            }
        }

        public string mtdInClearText
        {
            get
            {
                return (this.strM_sInClearText);
            }
            set
            {
                // assign value only if it is a new value
                if (this.strM_sInClearText != value)
                {
                    this.strM_sInClearText = value;
                }
            }
        }

        public string CryptedText
        {
            get
            {
                return (this.strM_sCryptedText);
            }
            set
            {
                // assign value only if it is a new value
                if (this.strM_sCryptedText != value)
                    this.strM_sCryptedText = value;
            }
        }
        #endregion

        #region Private Fields

        // Encryption Key  - Unicode & Ascii version
        private string strM_sEncryptionKey = string.Empty;
        private string strM_sEncryptionKeyAscii = string.Empty;

        // It is related to Encryption Key
        protected byte[] bytM_nBox = new byte[lonM_nLength];

        // Len of nBox
        static public long lonM_nLength = 255;

        // In Clear Text
        private string strM_sInClearText = string.Empty;
        private string strM_sCryptedText = string.Empty;

        #endregion

    }
}
