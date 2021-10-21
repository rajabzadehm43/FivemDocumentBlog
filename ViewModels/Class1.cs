using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Flurl.Http;

namespace ViewModels
{

    public class MyClass
    {
        public Class1 Course { get; set; }
    }

    public class Class1
    {
        public int Price { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        public string ImageUrl { get; set; }

        public string Info { get; set; }
    }
}
