using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Formatters
{
    public class VcardOutputFormatter : TextOutputFormatter
    {
        

        public VcardOutputFormatter()
        {
            

            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/vcard").ToString()); // Yeni bir header tanımlanır.
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);

        }


        public override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;

            var stringBuilder = new StringBuilder();

            if(context.Object is List<ContractModel>)
            {
                foreach (ContractModel model in context.Object as List<ContractModel>)
                {
                    FormatVcard(stringBuilder, model);
                }
            }
            else
            {
                var model = context.Object as ContractModel;
                FormatVcard(stringBuilder, model);
            }
            return response.WriteAsync(stringBuilder.ToString());
        }

        //Vcard formatının tasarlandığı static metod
        public static void FormatVcard(StringBuilder stringBuilder, ContractModel model) //
        {
            stringBuilder.AppendLine("Begin:VCARD");
            stringBuilder.AppendLine($"NAME:{model.FirstName}");
            stringBuilder.AppendLine($"LASTNAME:{model.LastName}");
            stringBuilder.AppendLine($"UID:{model.Id}\r\n");
            stringBuilder.AppendLine("END:VCARD");
        }

        protected override bool CanWriteType(Type type)
        {
            if(typeof(ContractModel).IsAssignableFrom(type)|| typeof(List<ContractModel>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
            
        }

    }
}
