using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data.Items;
using Sitecore.Forms.Mvc.Models.Fields;
using Sitecore.Support.Forms.Mvc.Validators;

namespace Sitecore.Support.Forms.Mvc.Models.Fields
{
    public class EmailField : SingleLineTextField
    {
        [EmailValidator]
        public override object Value
        {
            get;
            set;
        }

        public EmailField(Item item) : base(item)
        {
        }
    }
}