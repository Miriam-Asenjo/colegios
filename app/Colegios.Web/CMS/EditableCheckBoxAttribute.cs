using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using N2;
using N2.Collections;
using N2.Details;

namespace Colegios.Web.CMS
{
    public class EditableCheckBoxListAttribute : AbstractEditableAttribute
    {
        public EditableCheckBoxListAttribute(Type elementType, string textField, string valueField)
        {
            ElementType = elementType;
            TextField = textField;
            ValueField = valueField;
        }

        public override void UpdateEditor(ContentItem item, Control editor)
        {
            var checkBoxList = editor as CheckBoxList;
            if (checkBoxList != null)
            {
                foreach (ListItem listItem in checkBoxList.Items)
                {
                    if (item[this.Name].ToString().Contains(listItem.Value))
                        listItem.Selected = true;
                }
            }
        }

        public override bool UpdateItem(ContentItem item, Control editor)
        {
            var checkBoxList = editor as CheckBoxList;
            var items = new ArrayList();

            if (checkBoxList != null)
            {
                foreach (ListItem listItem in checkBoxList.Items)
                {
                    if (listItem.Selected)
                        items.Add(listItem.Value);
                }
            }

            var itemId = (string[])items.ToArray(typeof(string));
            item[this.Name] = String.Join(",", itemId); return true;
        }

        protected override Control AddEditor(Control container)
        {
            // here we create the editor control and add it to the page
            var checkBoxList = new CheckBoxList { DataTextField = TextField, DataValueField = ValueField };
            checkBoxList.DataSource = N2.Find.Items.Where.Type.Eq(ElementType).Filters(new PublishedFilter()).OrderBy.Title.Asc.Select();
            checkBoxList.DataBind();
            container.Controls.Add(checkBoxList);
            return checkBoxList;

        }

        private Type ElementType { set; get; }
        private string TextField { set; get; }
        private string ValueField { set; get; }

    }
}