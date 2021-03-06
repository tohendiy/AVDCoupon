﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using ADVCoupon.Models;
using ADVCoupon.Services;

namespace ADVCoupon.Controllers
{
    [Route("api/networks")]
    public class NetworksApiController : Controller
    {
        private INetworkService _service;
        public NetworksApiController(INetworkService service)
        {
            _service = service;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IEnumerable<Network>> GetNetworks()
        {
            var networks = await _service.GetNetworksAsync();
            return networks;
        }
        [HttpGet("{id}")]
        public async Task<List<Network>> GetNetworksByProductCategory(Guid id)
        {
            var networks = await _service.GetNetworksByProductCategoryAsync(id);
            return networks;
        }
        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<Network> GetNetwork(Guid id)
        {
            var network = await _service.GetNetwork(id);
            return network;
        }

        [HttpGet("{id}")]
        public async Task<Network> GetNetworkWithAdresses(Guid id)
        {
            var network = await _service.GetNetworkWithAdressesAsync(id);
            return network;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
