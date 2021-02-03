using RepositoryLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer.ViewModels;
using ModelLayer;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;

namespace LogicLayer
{
    public class LogicClass
    {
        private readonly Repo _repo;
        private readonly Mapper _mapper;
        private readonly UserManager<CustomUser> _userManager;
        private readonly SignInManager<CustomUser> _signInManager;
        public LogicClass(Repo repo, Mapper mapper, UserManager<CustomUser> userManager, SignInManager<CustomUser> signInManager)
        {
            _repo = repo;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public List<Location> GetLocations()
        {
            List<Location> locationList = _repo.locations.Where(t => t != null).ToList();
            return locationList;
        }
        public UserViewModel DisplayUser(Claim claim)
        {
            CustomUser user = new CustomUser();
            if (claim != null)
            {
                user = _repo.GetUserById(claim.Value);
            }
            UserViewModel uvm = _mapper.ConvertUserToViewModel(user, GetLocationById(user.DefaultStore));
            return uvm;
        }
        public List<LocationInventoryViewModel> DisplayLocationInventory(CustomUser user)
        {
            List<LocationInventoryViewModel> livm = new List<LocationInventoryViewModel>();
            foreach (Product prod in _repo.products)
            {
                var l = _repo.locationInventories.FirstOrDefault(l => l.LocationId == user.DefaultStore && l.ProductId == prod.ProductId);
                var s = GetLocationById(user.DefaultStore);
                livm.Add(_mapper.CreateLocationInventoryViewModel(l, s, prod));
            }
            return livm;
        }
        public CustomUser GetUserById(string id)
        {
            return _repo.customUsers.FirstOrDefault(user => user.Id == id);
        }
        public Location GetLocationById(int id)
        {
            return _repo.locations.FirstOrDefault(location => location.LocationId == id);
        }
        public Order GetOrderById(Guid id)
        {
            return _repo.orders.FirstOrDefault(x => x.OrderId == id);
        }
        public void SetUserDefaultStore(CustomUser user, int id)
        {
            CustomUser tUser = _repo.customUsers.FirstOrDefault(t => t.Id == user.Id);
            Cart tCart = _repo.carts.FirstOrDefault(s => s.Id == tUser.Id && s.LocationId == id);
            if (tCart == null)
            {
                tCart = new Cart
                {
                    Id = user.Id,
                    LocationId = id
                };
                InitializeCart(tCart);
            }
            tUser.DefaultStore = id;
            tUser.CartId = tCart.CartId;
            _repo.CommitSave();
        }
        public CustomUser GetCurrentUser(ClaimsIdentity claimsIdentity)
        {
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            CustomUser user = new CustomUser();
            if (claim != null)
            {
                user = _repo.GetUserById(claim.Value);
            }
            return user;
        }
        public List<LocationInventoryViewModel> AddToCart(List<LocationInventoryViewModel> livmList, LocationInventoryViewModel livm, CustomUser user)
        {
            var iloc = livm.Location.LocationId;
            var uloc = user.DefaultStore;
            CartInventory cInv = _repo.cartInventories.FirstOrDefault(x => x.CartId == user.CartId && x.ProductId == livm.ProductId);
            if (cInv == null)
            {
                cInv = new CartInventory
                {
                    CartId = user.CartId,
                    ProductId = livm.ProductId
                };
                _repo.cartInventories.Add(cInv);
            }
            LocationInventory lInv = _repo.locationInventories.FirstOrDefault(z => z.LocationId == livm.Location.LocationId && z.ProductId == livm.ProductId);
            if (cInv != null && lInv.Quantity >= livm.purchaseQuantity)
            {
                cInv.CartQuantity += livm.purchaseQuantity;
                lInv.Quantity -= livm.purchaseQuantity;
                livm.Quantity -= livm.purchaseQuantity;
                livm.purchaseQuantity = 0;
                _repo.CommitSave();
            }
            var l = livmList.FirstOrDefault(x => x.ProductId == livm.ProductId);
            l = livm;
            return livmList;
        }
        public void EmptyCurrentCart(CustomUser user)
        {
            foreach (CartInventory c in _repo.cartInventories)
            {
                if (c.CartId == user.CartId)
                {
                    _repo.cartInventories.Remove(c);
                }
            }
            //foreach (LocationInventory inv in _repo.locationInventories)
            //{
            //    if(inv.Loca)
            //}
            foreach (Cart c in _repo.carts)
            {
                if (c.CartId == user.CartId)
                {
                    _repo.carts.Remove(c);
                }
            }
            Cart cart = new Cart()
            {
                Id = user.Id,
                LocationId = user.DefaultStore
            };
            _repo.carts.Add(cart);
            user.CartId = cart.CartId;
            _repo.CommitSave();
        }
        public void InitializeCart(Cart cart)
        {
            _repo.carts.Add(cart);

            _repo.CommitSave();
        }
        public void SetUserFirstName(CustomUser user, string firstName)
        {
            CustomUser tUser = _repo.customUsers.FirstOrDefault(t => t.Id == user.Id);
            tUser.FirstName = firstName;
            _repo.CommitSave();
        }
        public void SetUserLastName(CustomUser user, string lastName)
        {
            CustomUser tUser = _repo.customUsers.FirstOrDefault(t => t.Id == user.Id);
            tUser.LastName = lastName;
            _repo.CommitSave();
        }
        public void SetUserAddress(CustomUser user, string address)
        {
            CustomUser tUser = _repo.customUsers.FirstOrDefault(t => t.Id == user.Id);
            tUser.Address = address;
            _repo.CommitSave();
        }
        public List<InvoiceViewModel> CreateInvoiceList(CustomUser user)
        {
            List<InvoiceViewModel> ivmList = new List<InvoiceViewModel>();
            List<CartInventory> cList = new List<CartInventory>();
            foreach (CartInventory c in _repo.cartInventories)
            {
                if (c.CartId == user.CartId)
                {
                    cList.Add(c);
                }
            }
            foreach (CartInventory c in _repo.cartInventories)
            { 
                Product p = _repo.products.FirstOrDefault(x => x.ProductId == c.ProductId);
                ivmList.Add(_mapper.CreateInvoiceViewModel(user, p, c.CartQuantity));
            }
            return ivmList;
        }
        public List<OrderInvoiceViewModel> CreateOrderInvoiceList(CustomUser user, Order order1)
        {
            List<OrderInvoiceViewModel> oivmList = new List<OrderInvoiceViewModel>();

            List<OrderInventory> oList = new List<OrderInventory>();
            foreach (OrderInventory o in _repo.orderInventories)
            {
                if (order1.Id == user.Id && o.OrderId == order1.OrderId)
                {
                    oList.Add(o);
                }
            }
            foreach (OrderInventory o in _repo.orderInventories)
            {
                if (o.OrderId == order1.OrderId)
                {
                    Product p = _repo.products.FirstOrDefault(x => x.ProductId == o.ProductId);
                    oivmList.Add(_mapper.CreateOrderInvoiceViewModel(user, p, o));
                }
            }
            return oivmList;
        }
        public Order PlaceOrder(CustomUser user)
        {
            List<InvoiceViewModel> ivmList = CreateInvoiceList(user);
            Order order = new Order()
            {
                OrderId = user.CartId,
                Id = user.Id,
                LocationId = ivmList.FirstOrDefault(x => x.Id == user.Id).LocationId,
                OrderTime = DateTime.Now,
                OrderTotal = ivmList.Sum(x => x.LineTotal)
            };
            _repo.orders.Add(order);
            foreach (InvoiceViewModel ivm in ivmList)
            {
                _repo.orderInventories.Add(new OrderInventory() { OrderId = order.OrderId, ProductId = ivm.Product.ProductId, OrderQuantity = ivm.Quantity });
            }
            EmptyCurrentCart(user);
            _repo.CommitSave();
            return order;
        }
        public List<Order> GetUserOrderHistory(CustomUser user)
        {
            List<Order> orders = new List<Order>();
            foreach (Order order in _repo.orders)
            {
                if (order.Id == user.Id)
                {
                    orders.Add(order);
                }
            }
            return orders;
        }
        public List<Order> GetLocationOrderHistory(CustomUser user)
        {
            List<Order> orders = new List<Order>();
            foreach (Order order in _repo.orders)
            {
                if (order.LocationId == user.DefaultStore)
                {
                    orders.Add(order);
                }
            }
            return orders;
        }
    }
}
