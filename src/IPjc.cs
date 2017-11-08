using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinWealthyCorgi
{
	internal interface IPjc
	{
		Task<double> GetBalanceAsync();
		Task<bool> TransferAsync(string address, double amount);
	}
}
