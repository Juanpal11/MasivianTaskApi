using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using ReoulleteApi.Contracts.Roullete;
using ReoulleteApi.Domain.Dtos;
using ReoulleteApi.Domain.Entities;
using ReoulleteApi.Entities;

namespace ReoulleteApi.Service
{
    public class RoulleteServices : IRoulleteServices
    {
        private readonly IDistributedCache distributedCache;

        public RoulleteServices(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        public string GenerateID() {
            try
            {
                var roullete = new Roullete();
                roullete.Open = true;
                string roulleteId = Guid.NewGuid().ToString("N");
                var value = JsonConvert.SerializeObject(roullete);
                this.distributedCache.SetString(roulleteId, value);
                string text = "The roullete was created successfully with id: " + roulleteId;
                SaveList(roullete.Open, roulleteId);

                return text;

            }
            catch (StackExchange.Redis.RedisTimeoutException timeout)
            {
                return "Error connecting redis: " + timeout;
            }
        }

        public string AllowDenyAccess(string roullete_Id) {
            try 
            {
                var redisRoulleteId = this.distributedCache.GetString(roullete_Id);
                var dataRoullete = JsonConvert.DeserializeObject<Roullete>(redisRoulleteId);

                if (dataRoullete.Open == true)
                {
                    return "The operation was successfull";
                }

                return "The operation was deny";
            }
            catch (StackExchange.Redis.RedisTimeoutException timeout)
            {
                return "Error connecting redis: " + timeout;
            }
        }

        public string Bet(string UserId, string roullete_Id, BetDto request)
        {
            try
            {
                var redisRoulleteId = this.distributedCache.GetString(roullete_Id);
                if (redisRoulleteId != null)
                {
                    var dataRoullete = JsonConvert.DeserializeObject<Roullete>(redisRoulleteId);
                    if (dataRoullete.Open == false)
                    {
                        return "This roullet has been closed";
                    }
                    var funds = request.CashAmount;
                    var number = request.Number;
                    dataRoullete.Users.Add(new User() { UserId = UserId, Funds = funds, Number = number });
                    dataRoullete.Open = true;
 
                    var value = JsonConvert.SerializeObject(dataRoullete);
                    this.distributedCache.SetString(roullete_Id, value);

                    return "Your bet was created successfully";
                }
                else
                {
                    return "This roullet Id it does not exist";
                }
            }
            catch (StackExchange.Redis.RedisTimeoutException timeout)
            {
                return "Error connecting redis: " + timeout;
            }
        }

        public string Close(string roullete_Id)
        {
            List<string> winners = new List<string>();
            var redisRoulleteId = this.distributedCache.GetString(roullete_Id);
            if (redisRoulleteId != null)
            {
                var dataRoullete = JsonConvert.DeserializeObject<Roullete>(redisRoulleteId);
                dataRoullete.Open = false;
                UpdateList(dataRoullete.Open, roullete_Id);
                int winnerNumber = RandomNumber(0, 36);
                string colorNumber = "black";
                if (winnerNumber % 2 == 0)
                {
                    colorNumber = "red";
                }

                foreach (User user in dataRoullete.Users)
                {
                    if (user.Number == winnerNumber)
                    {
                        user.Funds *= 5;
                        winners.Add(user.UserId);
                        continue;
                    }
                    if (user.Number == 37 && colorNumber == "black")
                    {
                        user.Funds *= 1.8f;
                        winners.Add(user.UserId);
                        continue;
                    }
                    if (user.Number == 38 && colorNumber == "red")
                    {
                        user.Funds *= 1.8f;
                        winners.Add(user.UserId);
                        continue;
                    }
                    else
                    {
                        user.Funds -= user.Funds;
                    }
                }
                var value = JsonConvert.SerializeObject(dataRoullete);
                this.distributedCache.SetString(roullete_Id, value);

                return "The winers are: " + string.Join(", ", winners);
            }

            return "This roullet id " + roullete_Id + " does not exist";
        }


        private readonly Random _random = new Random();
   
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }

        public string SaveList(bool isOpen, string roullete_Id)
        {
            RoulleteList list = new RoulleteList();
            string keyName = "roulletes";
            try
            {
                var redisRoulletes = this.distributedCache.GetString(keyName);
                if (redisRoulletes != null)
                {
                    var dataRoullete = JsonConvert.DeserializeObject<RoulleteList>(redisRoulletes);
                    dataRoullete.List.Add(new ToList() { RoulleteId = roullete_Id, Open = isOpen });

                    var value = JsonConvert.SerializeObject(dataRoullete);
                    this.distributedCache.SetString(keyName, value);
                }
                else
                {
                    var value = JsonConvert.SerializeObject(list);
                    this.distributedCache.SetString(keyName, value);
                }

                return "List saved successfully";
            }
            catch (StackExchange.Redis.RedisTimeoutException timeout)
            {
                return "Error connecting redis: " + timeout;
            }
        }

        public string UpdateList(bool isOpen, string roullete_Id)
        {
            try
            {
                string keyName = "roulletes";
                var redisRoulletes = this.distributedCache.GetString(keyName);
                if (redisRoulletes != null)
                {
                    var dataRoullete = JsonConvert.DeserializeObject<RoulleteList>(redisRoulletes);
                    for (int i = 0; i < dataRoullete.List.Count(); i++)
                    {
                        if (dataRoullete.List[i].RoulleteId == roullete_Id)
                        {
                            dataRoullete.List[i].Open = isOpen;
                        }
                    }

                    var value = JsonConvert.SerializeObject(dataRoullete);
                    this.distributedCache.SetString(keyName, value);
                }

                return "List was updated";
            }
            catch (StackExchange.Redis.RedisTimeoutException timeout)
            {
                return "Error connecting redis: " + timeout;
            }
        }

        public string ListRoulletes()
        {
            string keyName = "roulletes";
            Dictionary<string, string> list = new Dictionary<string, string>();
            var redisRoulletes = this.distributedCache.GetString(keyName);
            if (redisRoulletes != null)
            {
                var open = string.Empty;
                var dataRoullete = JsonConvert.DeserializeObject<RoulleteList>(redisRoulletes);
                for (int i = 0; i < dataRoullete.List.Count(); i++)
                {
                    if (dataRoullete.List[i].Open)
                    {
                        open = "open";
                    }
                    else
                    {
                        open = "closed";
                    }
                    var id = dataRoullete.List[i].RoulleteId;
                    list.Add(id, open);
                    
                }
                string dictionaryString = "";
                foreach (KeyValuePair<string, string> keyValues in list)
                {
                    dictionaryString += "Roullete Id: " + keyValues.Key + " Status : " + keyValues.Value + ", \n";
                }
                return dictionaryString.TrimEnd(',', ' ');
            }

            return "Error processing roullete list";
        }
    }

}

