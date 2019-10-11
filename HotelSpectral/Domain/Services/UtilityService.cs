using System;
using System.Security.Cryptography;
using System.Text;

namespace HotelSpectral.Domain.Services
{
    public class UtilityService
    {
        
        public static string EncryptPassword(string password, string salt = "")
        {
            string text = salt + password;
            var UE = new UTF8Encoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);

            SHA512Managed hashString = new SHA512Managed();
            string hex = "";

            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        protected internal string GenerateUnique()
        {
            uint RandomInt(RandomNumberGenerator rng)
            {
                var intByte = new byte[4];
                rng.GetBytes(intByte);
                return BitConverter.ToUInt32(intByte, 0);
            }
            using (var rng = new RNGCryptoServiceProvider())
            {
                var i1 = Math.Abs(RandomInt(rng));
                var i2 = Math.Abs(RandomInt(rng));
                return $"U-{DateTime.Now.ToString("yyddMM")}-{i1.ToString("X")}";
            }
        }

        public String GuestNo
        {
            get
            {
                return DateTime.Now.Ticks.ToString().Substring(10);
            }
        }

    }
}
