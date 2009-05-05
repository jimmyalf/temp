using System;
using Spinit.Wpc.Synologen.Business.Enumeration;

namespace Spinit.Wpc.Synologen.Business.Utility {
	public class Numerical {
		public static decimal RoundValue(decimal value, RoundDecimals roundSetting) {
			decimal returnValue;
			switch (roundSetting) {
				case RoundDecimals.DoNotRound:
					returnValue = value;
					break;
				case RoundDecimals.RoundUp:
					returnValue = Math.Ceiling(value);
					break;
				case RoundDecimals.RoundDown:
					returnValue = Math.Floor(value);
					break;
				case RoundDecimals.RoundWithTwoDecimals:
					returnValue = Math.Round(value, 2);
					break;
				default:
					throw new ArgumentOutOfRangeException("roundSetting");
			}
			return returnValue;
		}

		public static float RoundValue(float value, RoundDecimals roundSetting) {
			decimal returnValue = Convert.ToDecimal(value);
			returnValue = RoundValue(returnValue, roundSetting);
			return Convert.ToSingle(returnValue);
		}
	}
}