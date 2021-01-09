using System.Net.Http;
// ReSharper disable CheckNamespace
// ReSharper disable InconsistentNaming V~V

namespace SkittlesPower {
    public static class HTTP {
        // this also might be bad? who knows. | 30 min later - after googling it *should* be safe to reuse HttpClient
        public static HttpClient Client = new HttpClient(); // exposed to add headers if needed
    }
}