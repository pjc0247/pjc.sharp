using System;
using System.IO.Pipes;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.Web3;
using Nethereum.JsonRpc.Client;
using Nethereum.JsonRpc.IpcClient;

namespace CoinWealthyCorgi
{
	public class Wallet
	{
		private static readonly string PipeName = "geth.ipc";

		private PjcInterface intf;
		private Web3 w3;

		private bool isActive = false;

		public Wallet()
		{
		}

		public Task ConnectAsync(string address, string password)
		{
			return ConnectAsync(address, password, 10);
		}
		public async Task ConnectAsync(string address, string password, int timeout)
		{
			isActive = false;

			var ipc = new IpcClient(PipeName);
			w3 = new Web3(ipc);

			var unlocked = await w3.Personal.UnlockAccount.SendRequestAsync(
				address, password, timeout);

			if (unlocked == false)
				throw new InvalidOperationException("Failed to unlock account.");

			intf = new PjcInterface(w3);

			isActive = true;
		}

		private void EnsureReadyToOperate()
		{
			if (intf == null && isActive == false)
				throw new InvalidOperationException("Not initialzed");
		}
		public async Task<bool> Transfer(string address, double amount)
		{
			EnsureReadyToOperate();

			return await intf.TransferAsync(address, amount);
		}
		public async Task<double> GetBalance()
		{
			EnsureReadyToOperate();

			return await intf.GetBalanceAsync();
		}
	}
}
