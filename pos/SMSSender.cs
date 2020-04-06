using System.Diagnostics;
using System.Net;
using System.Web;

namespace pos
{
    public static class SMSSender
    {
        private static string username = "New_city";
        private static string password = "NCI@123";
        private static string callerId = "CAR WEST";
        public static void SendWebRequest(string DNumber, string Message)
        {
            string request = "https://bulksms2.etisalat.lk/sendsmsmultimask.php?";
            if (DNumber.StartsWith("0"))
            {
                DNumber = DNumber.Remove(0, 1);
            }
            if (!DNumber.StartsWith("94"))
            {
                DNumber = "94" + DNumber;
            }
            var postData = "USER=" + HttpUtility.UrlPathEncode(username) + "&PWD=" + HttpUtility.UrlPathEncode(password) + "&MASK=" + HttpUtility.UrlPathEncode(callerId) + "&NUM=" + HttpUtility.UrlPathEncode(DNumber) + "&MSG=" + HttpUtility.UrlDecode(Message);
            using (var web = new WebClient())
            {
                Debug.WriteLine(request + postData);
                string result = web.DownloadString(request + postData);
            }
        }
    }
}
