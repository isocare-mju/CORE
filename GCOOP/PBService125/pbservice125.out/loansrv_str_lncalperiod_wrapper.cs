using System.ServiceModel; 
using System.Runtime.Serialization; 
namespace Sybase.PowerBuilder.WCFNVO
{
	[DataContract]
	public struct str_lncalperiod
	{
		internal str_lncalperiod(c__str_lncalperiod __x__)
		{
			CopyFrom(out this, __x__);
		}
		internal void CopyFrom(c__str_lncalperiod __x__)
		{
			CopyFrom(out this, __x__);
		}
		[DataMember] 
		public string loantype_code;
		[DataMember] 
		public System.Int16 loanpayment_type;
		[DataMember] 
		public System.Int16 calperiod_maxinstallment;
		[DataMember] 
		public decimal calperiod_prnamt;
		[DataMember] 
		public decimal calperiod_intrate;
		[DataMember] 
		public System.Int16 period_installment;
		[DataMember] 
		public decimal period_payment;
		[DataMember] 
		public decimal period_lastpayment;
		internal void CopyTo(c__str_lncalperiod __x__)
		{
			__x__.loantype_code = loantype_code;
			__x__.loanpayment_type = loanpayment_type;
			__x__.calperiod_maxinstallment = calperiod_maxinstallment;
			__x__.calperiod_prnamt = calperiod_prnamt;
			__x__.calperiod_intrate = calperiod_intrate;
			__x__.period_installment = period_installment;
			__x__.period_payment = period_payment;
			__x__.period_lastpayment = period_lastpayment;
		}
		internal static void CopyFrom(out str_lncalperiod __this__, c__str_lncalperiod __x__)
		{
			__this__.loantype_code = __x__.loantype_code;
			__this__.loanpayment_type = __x__.loanpayment_type;
			__this__.calperiod_maxinstallment = __x__.calperiod_maxinstallment;
			__this__.calperiod_prnamt = __x__.calperiod_prnamt;
			__this__.calperiod_intrate = __x__.calperiod_intrate;
			__this__.period_installment = __x__.period_installment;
			__this__.period_payment = __x__.period_payment;
			__this__.period_lastpayment = __x__.period_lastpayment;
		}
		public static explicit operator object[](str_lncalperiod __this__)
		{
			return new object[] {
				__this__.loantype_code
				,__this__.loanpayment_type
				,__this__.calperiod_maxinstallment
				,__this__.calperiod_prnamt
				,__this__.calperiod_intrate
				,__this__.period_installment
				,__this__.period_payment
				,__this__.period_lastpayment
			};
		}
	}
} 