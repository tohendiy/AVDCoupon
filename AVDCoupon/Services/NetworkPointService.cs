using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADVCoupon.Models;
using ADVCoupon.ViewModel.NetworkPointViewModels;
using AVDCoupon.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ADVCoupon.Services
{
    public class NetworkPointService : INetworkPointService
    {
        private ApplicationDbContext _context;
        public NetworkPointService(ApplicationDbContext context)
        {
            _context = context;
        }
        public NetworkPointViewModel ConvertFromNetworkPointToViewModel(NetworkPoint networkPoint)
        {
            var networkPointModel = new NetworkPointViewModel()
            {
                Id = networkPoint.Id,
                Name = networkPoint.Name,
                Latitude = networkPoint.Geoposition?.Latitude,
                Longitude = networkPoint.Geoposition?.Longitude,
                Country = networkPoint.Geoposition?.Country,
                Region = networkPoint.Geoposition?.Region,
                City = networkPoint.Geoposition?.City,
                Address = networkPoint.Geoposition?.Address
            };
            return networkPointModel;
        }

        public NetworkPoint ConvertFromViewModelToNetworkPoint(NetworkPointViewModel networkPointModel)
        {
            var networkPoint = new NetworkPoint()
            {
                Id = networkPointModel.Id,
                Name = networkPointModel.Name,
                Geoposition = new Geoposition()
                {
                    Longitude = networkPointModel.Longitude,
                    Latitude = networkPointModel.Latitude,
                    Country = networkPointModel.Country,
                    Region = networkPointModel.Region,
                    City = networkPointModel.City,
                    Address = networkPointModel.Address
                }
            };
            return networkPoint;
        }

        public async Task<NetworkPoint> CreateNetworkPointAsync(NetworkPoint networkPoint)
        {
            networkPoint.Id = Guid.NewGuid();
            _context.Add(networkPoint);
            await _context.SaveChangesAsync();
            return networkPoint;
        }

        public async Task<NetworkPoint> CreateNetworkPointAsync(NetworkPointViewModel networkPointModel)
        {
            var networkPoint = new NetworkPoint
            {
                Name = networkPointModel.Name,
                Id = Guid.NewGuid(),
                Geoposition = new Geoposition()
                {
                    Longitude = networkPointModel.Longitude,
                    Latitude = networkPointModel.Latitude,
                    Country = networkPointModel.Country,
                    Region = networkPointModel.Region,
                    City = networkPointModel.City,
                    Address = networkPointModel.Address,
                    Id = Guid.NewGuid()
                },
                Network = _context.Networks.FirstOrDefault(item=> item.Id == networkPointModel.NetworkId)

            };

            _context.Add(networkPoint);
            await _context.SaveChangesAsync();
            return networkPoint;

        }

        public async Task DeleteNetworkPointAsync(Guid Id)
        {
            var networkPoint = await _context.NetworkPoints.SingleOrDefaultAsync(m => m.Id == Id);
            _context.NetworkPoints.Remove(networkPoint);
            await _context.SaveChangesAsync();
        }

        public async Task<NetworkPoint> GetNetworkPoint(Guid Id)
        {
            var networkPoint = await _context.NetworkPoints.Include(item => item.Geoposition).Include(item=>item.Network)
               .SingleOrDefaultAsync(m => m.Id == Id);
            return networkPoint;
        }

        public async Task<NetworkPointViewModel> GetNetworkPointViewModelAsync(Guid Id)
        {
            var networkPoint = await _context.NetworkPoints.Include(item => item.Geoposition).Include(item => item.Network)
                .SingleOrDefaultAsync(m => m.Id == Id);
            if (networkPoint == null)
            {
                return null;
            }
            var networkPointModel = new NetworkPointViewModel
            {
                Id = networkPoint.Id,
                Name = networkPoint.Name,
                NetworkName = networkPoint.Network.Caption,
                Latitude = networkPoint.Geoposition?.Latitude,
                Longitude = networkPoint.Geoposition?.Longitude,
                Country = networkPoint.Geoposition?.Country,
                Region = networkPoint.Geoposition?.Region,
                City = networkPoint.Geoposition?.City,
                Address = networkPoint.Geoposition?.Address,
                Networks = GetSelectListNetworks(),
                NetworkId = networkPoint.Network.Id
            };
            return networkPointModel;
        }

        public async Task<List<NetworkPoint>> GetNetworkPointsAsync()
        {
            var networkPoints = await _context.NetworkPoints.Include(item => item.Geoposition).ToListAsync();
            return networkPoints;
        }

        public async Task<NetworkPointViewModel> GetNetworkPointNetworkListItemViewModelAsync()
        {
            var networkModel = new NetworkPointViewModel();
            networkModel.Networks = GetSelectListNetworks();
            return networkModel;
        }

        public async Task<List<NetworkPointViewModel>> GetNetworkPointViewModelsAsync()
        {
            var networkPoints = await _context.NetworkPoints.Include(item => item.Geoposition).Include(item => item.Network).ToListAsync();
            var networkPointsListViewModel = new List<NetworkPointViewModel>(networkPoints.Count);
            networkPointsListViewModel = networkPoints.Select(item => new NetworkPointViewModel
            {
                Id = item.Id,
                Name = item.Name,
                NetworkName = item.Network.Caption,
                Latitude = item.Geoposition?.Latitude,
                Longitude = item.Geoposition?.Longitude,
                Country = item.Geoposition?.Country,
                Region = item.Geoposition?.Region,
                City = item.Geoposition?.City,
                Address = item.Geoposition?.Address

            }).ToList();
            return networkPointsListViewModel;
        }

        public bool IsExist(Guid Id)
        {
            return _context.NetworkPoints.Any(e => e.Id == Id);
        }

        public async Task UpdateNetworkPointAsync(NetworkPoint networkPoint)
        {
            _context.Update(networkPoint);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateNetworkPointAsync(NetworkPointViewModel networkPointModel)
        {
            var networkPoint = await GetNetworkPoint(networkPointModel.Id);
            networkPoint.Name = networkPointModel.Name;
            networkPoint.Geoposition.Longitude = networkPointModel.Longitude;
            networkPoint.Geoposition.Latitude = networkPointModel.Latitude;
            networkPoint.Geoposition.Country = networkPointModel.Country;
            networkPoint.Geoposition.Region = networkPointModel.Region;
            networkPoint.Geoposition.City = networkPointModel.City;
            networkPoint.Geoposition.Address = networkPointModel.Address;
            networkPoint.Network = _context.Networks.FirstOrDefault(item => item.Id == networkPointModel.NetworkId);
            _context.Update(networkPoint);
            await _context.SaveChangesAsync();
        }

        public async Task AddNetworkPoints(Guid networkId, List<NetworkPoint> list)
        {
			var existingNetworkPoints = _context.Networks.Where(item => item.Id == networkId).SelectMany(item1 => item1.NetworkPoints);
			_context.NetworkPoints.RemoveRange(existingNetworkPoints);
            await _context.NetworkPoints.AddRangeAsync(list);
            await _context.SaveChangesAsync();
        }


        public SelectList GetSelectListNetworks()
        {
            var networks = _context.Networks.Select(x => new { Id = x.Id, Value = x.Caption });
            var networksSelectList = new SelectList(networks, "Id", "Value");
            return networksSelectList;
        }
    }
}
