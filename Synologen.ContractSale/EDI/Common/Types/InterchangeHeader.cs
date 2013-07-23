using System;

namespace Spinit.Wpc.Synologen.EDI.Common.Types {
	public class InterchangeHeader {
		private const int DefaultControlReferenceLength = 5;
		private readonly string randomControlReference;
		private readonly DateTime createDate;

		public InterchangeHeader() {
			randomControlReference = EDIUtility.GetNewRandomAlphaNumericString(DefaultControlReferenceLength, true);
			createDate = DateTime.Now;
		}
		public InterchangeHeader(int controlReferenceLength) {
			randomControlReference = EDIUtility.GetNewRandomAlphaNumericString(controlReferenceLength, true);
			createDate = DateTime.Now;
		}
		public InterchangeHeader(DateTime createDateTime) {
			randomControlReference = EDIUtility.GetNewRandomAlphaNumericString(DefaultControlReferenceLength, true);
			createDate = createDateTime;
		}
		public InterchangeHeader(int controlReferenceLength, DateTime createDateTime) {
			randomControlReference = EDIUtility.GetNewRandomAlphaNumericString(controlReferenceLength, true);
			createDate = createDateTime;
		}

		public string SenderId { get; set; }
		public string RecipientId { get; set; }
		public DateTime DateOfPreparation {
			get { return createDate; }
		}
		public string ControlReference {
			get { return randomControlReference; }
		}
		public bool IsEmpty {
			get {
				if((SenderId + RecipientId + ControlReference).Length >= 0) return false;
				return (DateOfPreparation != DateTime.MinValue);
			}
		}
	}
}