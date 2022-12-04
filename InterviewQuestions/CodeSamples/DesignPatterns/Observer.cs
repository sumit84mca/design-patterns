
using System;
using System.Collections.Generic;
namespace ObserverPattern
{
    public class MyPropertyBag
    {
        private IDictionary<string, object> properties;
        public MyPropertyBag()
        {
            properties = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);
        }

        public void SetProperty(string key, Object value)
        {
            if (properties.ContainsKey(key))
            {
                properties[key] = value;
            }
            else
            {
                properties.Add(key, value);
            }
        }

        public object GetProperty(string key)
        {
            return properties.TryGetValue(key, out object newValue) ? newValue : null;
        }
    }

    /// <summary>
    /// Subject which will be observed by client. 
    /// Or you can say this will be sending upated to its clients
    /// </summary>    
    public interface IStockPriceObservable
    {
        void AddToObserversList(IStockPriceObserver stockPriceObserver);
        void RemoveFromObserversList(IStockPriceObserver stockPriceObserver);
        void UpdateStockPrice(decimal newPrice);
    }

    public class Stock : IStockPriceObservable
    {
        List<IStockPriceObserver> stockPriceObservers;
        public string StockName { get; }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        public Stock(string stockName)
        {
            this.StockName = stockName;
            stockPriceObservers = new List<IStockPriceObserver>();
        }

        public void AddToObserversList(IStockPriceObserver stockPriceObserver)
        {
            stockPriceObservers.Add(stockPriceObserver);
        }

        public void RemoveFromObserversList(IStockPriceObserver stockPriceObserver)
        {
            stockPriceObservers.Remove(stockPriceObserver);
        }

        public void UpdateStockPrice(decimal newPrice)
        {
            this.price = newPrice;
            MyPropertyBag propertyBag = new MyPropertyBag();
            propertyBag.SetProperty("stockPrice", newPrice);
            stockPriceObservers.ForEach(p => p.Update(propertyBag));
        }
    }


    /// <summary>
    /// These will listen to the updates coming from the Observable
    /// </summary>
    public interface IStockPriceObserver
    {
        void Subscribe(IStockPriceObservable observable);
        void UnSubscribe();
        void Update(MyPropertyBag propertyBag);
    }

    public class StockChart : IStockPriceObserver
    {
        private IStockPriceObservable currentobservable;

        public void Subscribe(IStockPriceObservable observable)
        {
            currentobservable = observable;
            currentobservable.AddToObserversList(this);
        }

        public void UnSubscribe()
        {
            currentobservable.RemoveFromObserversList(this);
        }

        public void Update(MyPropertyBag propertyBag)
        {
            decimal newPrice = (decimal)propertyBag.GetProperty("stockprice");
            Console.WriteLine($"updating chart as its price changed to {newPrice}");
        }
    }

    public class StockProfitLoss : IStockPriceObserver
    {
        private IStockPriceObservable currentobservable;

        public void Subscribe(IStockPriceObservable observable)
        {
            currentobservable = observable;
            currentobservable.AddToObserversList(this);
        }

        public void UnSubscribe()
        {
            currentobservable.RemoveFromObserversList(this);
        }

        public void Update(MyPropertyBag propertyBag)
        {
            decimal newPrice = (decimal)propertyBag.GetProperty("stockprice");
            Console.WriteLine($"updating profit and loss as its price changed to {newPrice}");
        }
    }

    public class ObserverClient
    {
        public static void ObserverMain()
        {
            IStockPriceObservable tataStockObservable = new Stock("Tata");

            IStockPriceObserver chartObserver = new StockChart();
            IStockPriceObserver profitLossObserver = new StockProfitLoss();

            chartObserver.Subscribe(tataStockObservable);
            profitLossObserver.Subscribe(tataStockObservable);
            tataStockObservable.UpdateStockPrice(10);

            Console.ReadLine();
        }
    }
}

