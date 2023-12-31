using System.ServiceModel; 
using System.Runtime.Serialization; 
using System.Net.Security; 
using System.ServiceModel.Web; 
using System.ServiceModel.Activation; 
using System.Transactions; 
using Sybase.PowerBuilder.WCFNVO; 
namespace pbservice125
{
	[System.Diagnostics.DebuggerStepThrough]
	[ServiceContract(Name="n_trading",Namespace="http://tempurl.org")]
	public class n_trading : System.IDisposable 
	{
		internal pbservice125.c__n_trading __nvo__;
		private bool ____disposed____ = false;
		public void Dispose()
		{
			if (____disposed____)
				return;
			____disposed____ = true;
			c__pbservice125.InitSession(__nvo__.Session);
			Sybase.PowerBuilder.WPF.PBSession.CurrentSession.DestroyObject(__nvo__);
			c__pbservice125.RestoreOldSession();
		}
		public n_trading()
		{
			
			c__pbservice125.InitAssembly();
			__nvo__ = (pbservice125.c__n_trading)Sybase.PowerBuilder.WPF.PBSession.CurrentSession.CreateInstance(typeof(pbservice125.c__n_trading));
			c__pbservice125.RestoreOldSession();
		}
		internal n_trading(pbservice125.c__n_trading nvo)
		{
			__nvo__ = nvo;
		}
		[OperationContract(Name="of_test")]
		public virtual string of_test(string as_test)
		{
			c__pbservice125.InitSession(__nvo__.Session);
			string __retval__;
			Sybase.PowerBuilder.PBString as_test__temp__;
			as_test__temp__ = new Sybase.PowerBuilder.PBString((string)as_test);
			__retval__ = ((pbservice125.c__n_trading)__nvo__).of_test(as_test__temp__);
			c__pbservice125.RestoreOldSession();
			return __retval__;
		}
	}
} 