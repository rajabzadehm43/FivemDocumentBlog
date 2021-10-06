using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using Models.DocsModels;

namespace ViewModels.Admin.Docs
{
    public class AdminIndexNativeViewModel
    {
        public int Page { get; set; }

        public int AllPages { get; set; }

        public string Q { get; set; }

        public List<Native> Natives { get; set; }
    }
}