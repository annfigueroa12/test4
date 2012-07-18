using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace hello_kendo_ui.Models {
    
    public class Response {

        public Array Data { get; set; }
        public int Count { get; set; }

        public Response(Array data, int count) {
            this.Data = data;
            this.Count = count;
        }
    }
}