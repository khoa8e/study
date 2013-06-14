using System.Globalization;

namespace EightElements.Web.SupportTool.Helpers
{
    public class General
    {
        public static string ToLowerCapitalizeFirstLetter(string input)
        {
            TextInfo UsaTextInfo = new CultureInfo("en-US", false).TextInfo;
            return UsaTextInfo.ToTitleCase(input.ToLower());
        }
    }    
}
