using System;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;

namespace Spinit.Wpc.Synologen.Presentation.Intranet.Logic.EventArguments.FrameOrders
{
    public class EditFrameFormEventArgs : EventArgs, 
        IFrameSelectedEventArgs, 
        IGlassTypeSelectedEventArgs
	{
		public EyeParameter SelectedPupillaryDistance { get; set; }
		public EyeParameter SelectedSphere { get; set; }
		public EyeParameter SelectedCylinder { get; set; }
		public EyeParameter<int> SelectedAxis { get; set; }
		public EyeParameter SelectedAddition { get; set; }
		public EyeParameter SelectedHeight { get; set; }
        public int SelectedSupplierId { get; set; }
        public int SelectedFrameId { get; set; }
        public int SelectedGlassTypeId { get; set; }
		public string Reference { get; set; }
		public bool PageIsValid { get; set; }
	}


    public class FrameOrGlassTypeSelectedEventArgs : SupplierSelectedEventArgs, IFrameSelectedEventArgs, IGlassTypeSelectedEventArgs
    {
        public int SelectedFrameId { get; set; }
        public int SelectedGlassTypeId { get; set; }
        public EyeParameter SelectedPupillaryDistance { get; set; }
        public EyeParameter SelectedSphere { get; set; }
        public EyeParameter SelectedCylinder { get; set; }
        public EyeParameter SelectedAddition { get; set; }
        public EyeParameter SelectedHeight { get; set; }      
    }


    //public class FrameSelectedEventArgs : GlassTypeSelectedEventArgs, IFrameSelectedEventArgs
    //{
    //    public int SelectedFrameId { get; set; }
    //    public EyeParameter SelectedPupillaryDistance { get; set; }
    //}

    public interface IFrameSelectedEventArgs : ISupplierSelectedEventArgs
    {
        int SelectedFrameId { get; set; }
        EyeParameter SelectedPupillaryDistance { get; set; }
    }

    //public class GlassTypeSelectedEventArgs : SupplierSelectedEventArgs, IGlassTypeSelectedEventArgs
    //{
    //    public int SelectedGlassTypeId { get; set; }
    //    public EyeParameter SelectedSphere { get; set; }
    //    public EyeParameter SelectedCylinder { get; set; }
    //}

    public interface IGlassTypeSelectedEventArgs : ISupplierSelectedEventArgs
    {
        int SelectedGlassTypeId { get; set; }
        EyeParameter SelectedSphere { get; set; }
        EyeParameter SelectedCylinder { get; set; }
        EyeParameter SelectedAddition { get; set; }
        EyeParameter SelectedHeight { get; set; }      
    }

    public class SupplierSelectedEventArgs : EventArgs, ISupplierSelectedEventArgs
    {
        public int SelectedSupplierId { get; set; }
    }

    public interface ISupplierSelectedEventArgs
    {
        int SelectedSupplierId { get; set; }
    }
}