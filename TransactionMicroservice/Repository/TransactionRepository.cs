using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TransactionMicroservice.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
         Uri baseAddress = new Uri("https://localhost:44356/api");   //Port No.
        HttpClient client;

        public TransactionRepository()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;

        }
        public transactionMessage deposit(transactionInput value)
        {
            transactionMessage ob = new transactionMessage();
            ob.Message = "Success";
            return ob;
        }

        public transactionMessage transfer(transfers value)
        {
            transactionInput ob4 = new transactionInput { accountId = value.senderAccountId, balance = value.amount };
            transactionMessage ob = new transactionMessage();
            string data = JsonConvert.SerializeObject(ob4);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Rules/evaluateMinBal/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                ruleStatus s1 = JsonConvert.DeserializeObject<ruleStatus>(data1);
                if (s1.message == "Warning")
                {
                    ob.Message = "Warning";
                    return ob;
                }
                ob.Message = "No Warning";
                return ob;
            }
            return null;
        }

        public transactionMessage withdraw(transactionInput value)
        {
            transactionMessage ob = new transactionMessage();
            string data = JsonConvert.SerializeObject(value);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Rules/evaluateMinBal/", content).Result;
            if (response.IsSuccessStatusCode)
            {
                string data1 = response.Content.ReadAsStringAsync().Result;
                ruleStatus s1 = JsonConvert.DeserializeObject<ruleStatus>(data1);
                if (s1.message == "Warning")
                {
                    ob.Message = "Warning";
                    return ob;
                }
                ob.Message = "No Warning";
                return ob;
            }
            return null;
        }
    }
}
