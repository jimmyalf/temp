using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

using Spinit.Extensions;
using Spinit.Wpc.Synologen.Core.Domain.Model.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Presenters.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Logic.Views.Deviations;
using Spinit.Wpc.Synologen.Presentation.Intranet.Models.Deviations;

using WebFormsMvp;
using WebFormsMvp.Web;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Wpc.Synologen.Deviations
{
    [PresenterBinding(typeof(CreateDeviationPresenter))]
    public partial class CreateDeviation : MvpUserControl<CreateDeviationModel>, ICreateDeviationView
    {
        public event EventHandler<CreateDeviationEventArgs> Submit;
        public event EventHandler<CreateDeviationEventArgs> CategorySelected;
        public event EventHandler<CreateDeviationEventArgs> TypeSelected;

        protected void Page_Load(object sender, EventArgs e)
        {
            WireupEventProxy();
            pnlInternalDeviationConfirmation.Visible = false;
            pnlExternalDeviationConfirmation.Visible = false;
        }

        private void WireupEventProxy()
        {
            drpTypes.SelectedIndexChanged += drpTypes_SelectedIndexChanged;
            drpCategories.SelectedIndexChanged += drpCategories_SelectedIndexChanged;
            btnConfirmInternalDeviation.Click += btnConfirmInternalDeviation_Click;
            btnConfirmExternalDeviation.Click += btnConfirmExternalDeviation_Click;
            btnSubmitInternal.Click += btnSubmit_OnClick;
            btnSubmitExternal.Click += btnSubmit_OnClick;
            btnChangeExternal.Click += btnChangeExternal_Click;
            btnChangeInternal.Click += btnChangeInternal_Click;
        }

        void drpTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var eventArgs = new CreateDeviationEventArgs
            {
                SelectedType = (DeviationType)drpTypes.SelectedValue.ToIntOrDefault(0)
            };
            TypeSelected(this, eventArgs);
        }

        void btnChangeInternal_Click(object sender, EventArgs e)
        {
            pnlCreateDeviationForm.Visible = true;
        }

        void btnChangeExternal_Click(object sender, EventArgs e)
        {
            pnlCreateDeviationForm.Visible = true;
        }

        private void btnSubmit_OnClick(object sender, EventArgs e)
        {
            var eventArgs = (CreateDeviationEventArgs)Page.Session["CreateDeviationEventArgs"];
            eventArgs.SendEmailSupplier = Convert.ToBoolean(chkSendEmailSupplier.Checked);
            Submit(this, eventArgs);
        }

        private void btnConfirmInternalDeviation_Click(object sender, EventArgs e)
        {
            Page.Session["CreateDeviationEventArgs"] = null;
            pnlInternalDeviationConfirmation.Visible = true;
            pnlCreateDeviationForm.Visible = false;

            lblTitle.Text = txtTitle.Text;
            lblInternalDefectDescription.Text = txtInternalDefectDescription.Text;

            Model.SelectedCategoryId = drpCategories.SelectedValue.ToIntOrDefault(0);

            var eventArgs = new CreateDeviationEventArgs
            {
                SelectedType = DeviationType.Internal,
                Title = txtTitle.Text,
                DefectDescription = txtInternalDefectDescription.Text
            };

            Page.Session["CreateDeviationEventArgs"] = eventArgs;
        }

        private void btnConfirmExternalDeviation_Click(object sender, EventArgs e)
        {
            Page.Session["CreateDeviationEventArgs"] = null;
            pnlExternalDeviationConfirmation.Visible = true;
            pnlCreateDeviationForm.Visible = false;

            var defects = AddDefectsToList(cblDefects);

            var eventArgs = new CreateDeviationEventArgs
            {
                SelectedType = DeviationType.External,
                SelectedCategory = drpCategories.SelectedValue.ToIntOrDefault(0),
                SelectedDefects = defects,
                DefectDescription = txtExternalDefectDescription.Text,
                SelectedSupplier = drpSuppliers.SelectedValue.ToIntOrDefault(0)
            };

            Page.Session["CreateDeviationEventArgs"] = eventArgs;

            lblSupplier.Text = drpSuppliers.SelectedItem.Text;
            lblExternalDeviationCategoryName.Text = drpCategories.SelectedItem.Text;
            lblExternalDefectDescription.Text = txtExternalDefectDescription.Text;
            dgExternalDeviationCategoryDefects.DataSource = defects;
            dgExternalDeviationCategoryDefects.DataBind();
        }

        private void drpCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CategorySelected == null)
                return;

            var eventArgs = new CreateDeviationEventArgs
            {
                SelectedCategory = drpCategories.SelectedValue.ToIntOrDefault(0),
                SelectedType = (DeviationType)drpTypes.SelectedValue.ToIntOrDefault(0)
            };

            CategorySelected(this, eventArgs);
        }

        private List<DeviationDefectListItem> AddDefectsToList(CheckBoxList checkBoxList)
        {
            var result = new List<DeviationDefectListItem>();
            for (var i = 0; i < checkBoxList.Items.Count; i++)
            {
                if (checkBoxList.Items[i].Selected)
                {
                    result.Add(new DeviationDefectListItem { Name = checkBoxList.Items[i].Text, Id = int.Parse(checkBoxList.Items[i].Value) });
                }
            }
            return result;
        }

    }
}