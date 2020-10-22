using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVCClient.Models.Kanban;
using MVCClient.Services;
using Newtonsoft.Json;
using Simple.OData.Client;

namespace MVCClient.Controllers
{
    public class KanbanTaskController : Controller
    {
        private readonly ILogger<KanbanTaskController> _logger;
        private readonly INisHttpClient _nisHttpClient;
        private readonly INisHttpOdataClient _nisHttpOdataClient;
        private readonly IConfiguration _configuration;

        public KanbanTaskController(INisHttpClient nisHttpClient, INisHttpOdataClient nisHttpOdataClient, IConfiguration configuration, ILogger<KanbanTaskController> logger)
        {
            _nisHttpClient = nisHttpClient;
            _nisHttpOdataClient = nisHttpOdataClient;
            _configuration = configuration;
            _logger = logger;
        }

        public  IActionResult Index()
        {


            var taskPriorities = _nisHttpOdataClient.GetClient().For<TableTaskPriorities>().FindEntriesAsync().Result;
            var taskReferenceTypes = _nisHttpOdataClient.GetClient().For<TableTaskReferenceTypes>().FindEntriesAsync().Result;
            var taskStatuses = _nisHttpOdataClient.GetClient().For<TableTaskStatuses>().FindEntriesAsync().Result;
            var taskTypes = _nisHttpOdataClient.GetClient().For<TableTaskTypes>().FindEntriesAsync().Result;

            ViewBag.Priorities = taskPriorities.Select(d => d.ShortDescription).ToList();
            ViewBag.ReferenceTypes = taskReferenceTypes.Select(d => d.ShortDescription).ToList();
            ViewBag.Statuses = taskStatuses.Select(d => d.ShortDescription).ToList();
            ViewBag.TaskTypes = taskTypes.Select(d => d.ShortDescription).ToList();

            return View();
        }

        [HttpPost]
        public async Task<List<KanbanCard>> LoadCard()
        {
            List<KanbanCard> kanbanCards = new List<KanbanCard>();
            var httpClient = await _nisHttpClient.GetClient();
            var response = await httpClient.GetAsync(_configuration["ApiResourceBaseUrls:Api"] + "/api/v1/PersonTasks").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                kanbanCards = JsonConvert.DeserializeObject<List<KanbanCard>>(apiResponse);
            }

            return kanbanCards;
        }

        [HttpPost]
        public async Task<List<KanbanCard>> UpdateCard([FromBody]EditParams param)
        {
            var httpClient = await _nisHttpClient.GetClient();

            // this block of code will execute while inserting the new cards
            if (param.action == "insert" || (param.action == "batch" && param.added.Count > 0))
            {
                var value = (param.action == "insert") ? param.value : param.added[0];
                //int intMax = _context.KanbanCards.ToList().Count > 0 ? _context.KanbanCards.ToList().Max(p => p.Id) : 1;
                KanbanCard card = new KanbanCard()
                {
                    Title = value.Title,
                    Status = value.Status,
                    Summary = value.Summary,
                    TaskType = value.TaskType,
                    Priority = value.Priority,
                    ReferenceEntity = value.ReferenceEntity,
                    ReferenceNumber = value.ReferenceNumber,
                    ReferenceDate = value.ReferenceDate,
                    DateToBeCompleted = value.DateToBeCompleted,
                    Colour = value.Colour
                };
                //var httpClient = await _nisHttpClient.GetClient();
                StringContent content = new StringContent(JsonConvert.SerializeObject(card), Encoding.UTF8, "application/json");
                var insertResponse = await httpClient.PostAsync(_configuration["ApiResourceBaseUrls:Api"] + "/api/v1/PersonTasks", content);

                if (!insertResponse.IsSuccessStatusCode)
                    throw new Exception(insertResponse.ToString());
            }
            // this block of code will execute while updating the existing cards
            if (param.action == "update" || (param.action == "batch" && param.changed.Count > 0))
            {
                KanbanCard value = (param.action == "update") ? param.value : param.changed[0];

                KanbanCard updateCard = new KanbanCard();
//                var httpClient = await _nisHttpClient.GetClient();
                var cardExistResponse = await httpClient.GetAsync(_configuration["ApiResourceBaseUrls:Api"] + "/api/v1/PersonTasks/" + value.ID).ConfigureAwait(false);

                if (cardExistResponse.IsSuccessStatusCode)
                {
                    var apiResponse = await cardExistResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                    updateCard = JsonConvert.DeserializeObject<KanbanCard>(apiResponse);
                }

                //IQueryable<KanbanCard> filterData = _context.KanbanCards.Where(c => c.Id == Convert.ToInt32(value.Id));
                if (updateCard.ID > 0)
                {
                    //KanbanCard card = _context.KanbanCards.Single(A => A.Id == Convert.ToInt32(value.Id));
                    updateCard.Title = value.Title;
                    updateCard.Status = value.Status;
                    updateCard.Summary = value.Summary;
                    updateCard.TaskType = value.TaskType;
                    updateCard.Priority = value.Priority;
                    updateCard.ReferenceEntity = value.ReferenceEntity;
                    updateCard.ReferenceNumber = value.ReferenceNumber;
                    updateCard.ReferenceDate = value.ReferenceDate;
                    updateCard.DateToBeCompleted = value.DateToBeCompleted;
                    updateCard.Colour = value.Colour;
                    updateCard.User = value.User;
                    updateCard.UserID = value.UserID;

                    //var httpClient = await _nisHttpClient.GetClient();
                    StringContent content = new StringContent(JsonConvert.SerializeObject(updateCard), Encoding.UTF8, "application/json");
                    var updateResponse = await httpClient.PutAsync(_configuration["ApiResourceBaseUrls:Api"] + "/api/v1/PersonTasks/" + updateCard.ID, content);

                    if (!updateResponse.IsSuccessStatusCode)
                        throw new Exception(updateResponse.ToString());

                }
                //                _context.SaveChanges();
            }
            // this block of code will execute while deleting the existing cards
            if (param.action == "remove" || (param.action == "batch" && param.deleted.Count > 0))
            {
                if (param.action == "remove")
                {
                    long key = Convert.ToInt64(param.key);

                    KanbanCard deleteCard = new KanbanCard();
//                    var httpClient = await _nisHttpClient.GetClient();
                    var deleteResponse = await httpClient.DeleteAsync(_configuration["ApiResourceBaseUrls:Api"] + "/api/v1/PersonTasks/" + key).ConfigureAwait(false);
                    if (!deleteResponse.IsSuccessStatusCode)
                        throw new Exception(deleteResponse.ToString());


                    //KanbanCard card = _context.KanbanCards.Where(c => c.Id == key).FirstOrDefault();
                    //if (card != null)
                    //{
                    //    _context.KanbanCards.Remove(card);
                    //}
                }
                else
                {
                    foreach (KanbanCard cards in param.deleted)
                    {
                        KanbanCard deleteCard = new KanbanCard();
//                        var httpClient = await _nisHttpClient.GetClient();
                        var deleteResponse = await httpClient.DeleteAsync(_configuration["ApiResourceBaseUrls:Api"] + "/api/v1/PersonTasks/" + cards.ID).ConfigureAwait(false);
                        if (!deleteResponse.IsSuccessStatusCode)
                            throw new Exception(deleteResponse.ToString());

                        //KanbanCard card = _context.KanbanCards.Where(c => c.Id == cards.Id).FirstOrDefault();
                        //if (cards != null)
                        //{
                        //    _context.KanbanCards.Remove(card);
                        //}
                    }
                }
                //_context.SaveChanges();
            }

            List<KanbanCard> kanbanCards = new List<KanbanCard>();
            //var httpClient = await _nisHttpClient.GetClient();
            var response = await httpClient.GetAsync(_configuration["ApiResourceBaseUrls:Api"] + "/api/v1/PersonTasks").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                kanbanCards = JsonConvert.DeserializeObject<List<KanbanCard>>(apiResponse);
            }

            return kanbanCards;

            //return _context.KanbanCards.ToList();
        }



        //private ODataClientSettings SetODataToken(string url, string accessToken)
        //{
        //    var oDataClientSettings = new ODataClientSettings(new Uri(url));
        //    oDataClientSettings.BeforeRequest += delegate (HttpRequestMessage message)
        //    {
        //        message.Headers.Add("Authorization", "Bearer " + accessToken);

        //    };
        //    oDataClientSettings.PayloadFormat = ODataPayloadFormat.Json;

        //    return oDataClientSettings;
        //}

        //var httpClient = await _nisHttpClient.GetClient();
        //var response = await httpClient.GetAsync(_configuration["ApiResourceBaseUrls:Api"] + "/odata/TableTaskPriorities").ConfigureAwait(false);

        //if (response.IsSuccessStatusCode)
        //{
        //    var apiResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        //}


        //var accessToken = HttpContext.GetTokenAsync("access_token").Result;
        //string url = _configuration["ApiResourceBaseUrls:Api"] + "/odata";

        //var client = new ODataClient(SetODataToken(url, accessToken));

        //var taskPriorities = await client.For<TableTaskPriorities>().FindEntriesAsync();

        //public class TableTaskPriorities
        //{
        //    public long ID { get; set; }
        //    public string Code { get; set; }
        //    public string ShortDescription { get; set; }
        //    public string LongDescription { get; set; }
        //}
    }

    public class EditParams
    {
        public string key { get; set; }
        public string action { get; set; }
        public string keyColumn { get; set; }
        public string table { get; set; }

        public List<KanbanCard> added { get; set; }
        public List<KanbanCard> changed { get; set; }
        public List<KanbanCard> deleted { get; set; }
        public KanbanCard value { get; set; }
    }

}