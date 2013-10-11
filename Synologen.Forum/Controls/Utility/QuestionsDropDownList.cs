using System;
using System.Web.UI.WebControls;
using Spinit.Wpc.Forum.Enumerations;
using Spinit.Wpc.Forum.Components;

namespace Spinit.Wpc.Forum.Controls {
    
    public class QuestionsDropDownList : DropDownList {

        public QuestionsDropDownList() {

            // Add QA variants
            //
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q1"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q1")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q2"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q2")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q3"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q3")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q4"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q4")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q5"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q5")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q6"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q6")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q7"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q7")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q8"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q8")));
            Items.Add(new ListItem(Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q9"), Spinit.Wpc.Forum.Components.ResourceManager.GetString("Utility_QuestionsDropDownList_Q9")));
        }

    }
}
