using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components;
using BlazorVirtualizeProject.Models;

namespace BlazorVirtualizeProject.Pages
{
    public partial class VirtualizeExamplePage
    {
        const int OverscanCount = 3; 
        public EventCallback LoadCarsEvent { get; set; }

        protected override Task OnInitializedAsync()
        {
            LoadCarsEvent.InvokeAsync();
            return base.OnInitializedAsync();
        }

        public async Task AnswerASelected()
        {
            await MakeCars();
        }      

        private static async Task<List<Car>> MakeCars()
        {
            List<Car> myCarList = new();

            for (int i = 0; i < 10000; i++)
            {
                var car = new Car()
                {
                    Id = Guid.NewGuid(),
                    Name = $"Car {i}",
                    Cost = i * 77 + 105
                };
                myCarList.Add(car);
            }
            return await Task.FromResult(myCarList);
        }

        private static async ValueTask<ItemsProviderResult<Car>> LoadCars(ItemsProviderRequest request)
        {
            var cars = await MakeCars();            
            await Task.Delay(10);
            return new ItemsProviderResult<Car>(cars.Skip(request.StartIndex).Take(request.Count), cars.Count);
        }
    }
}
