using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.JsonRpc.IpcClient;
using Nethereum.Web3;
using Nethereum.Contracts;

namespace CoinWealthyCorgi
{
	internal class PjcInterface : IPjc
	{
		private static readonly string ContractAddress = "0x9e57d667D107aA2dC5b6D653353e152f84A5D620";
		private static readonly string ABI = "";

		private Contract contract;
		private Web3 w3;

		#region FUNCTIONS
		private Function getBalance;
		private Function transfer;
		#endregion

		public PjcInterface(Web3 w3)
		{
			this.w3 = w3;
			this.contract = w3.Eth.GetContract(ABI, ContractAddress);

			getBalance = contract.GetFunction("balance");
		}

		public async Task<double> GetBalanceAsync()
		{
			return await getBalance.CallDeserializingToObjectAsync<double>();
		}

		public async Task<bool> TransferAsync(string address, double amount)
		{
			return await transfer.CallAsync<bool>(address, amount);
		}
	}
}
