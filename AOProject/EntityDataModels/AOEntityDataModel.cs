using AOProject.Entities;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;

namespace AOProject.EntityDataModels
{
    public class AOEntityDataModel
    {
        public IEdmModel GetEntityDataModel()
        {
            var builder = new ODataConventionModelBuilder();
         //   builder.Namespace = "AccountOpening";
           // builder.ContainerName = "AccountOpeningContainer";

           // builder.EntitySet<Account>("Accounts");
            builder.EntitySet<Customer>("Customers");
            builder.EntitySet<Customer>("Branches");

            var isHighRatedFunction = builder.EntityType<Branch>()
                .Function("IsHighRated");
            isHighRatedFunction.Returns<bool>();
            isHighRatedFunction.Parameter<int>("minimumRating");
            //isHighRatedFunction.Namespace = "AccountOpening.Functions";

            /* var areRatedByFunction = builder.EntityType<Branch>().Collection
                 .Function("areRatedBy");
             areRatedByFunction.ReturnsCollectionFromEntitySet<Branch>("Branches");
             areRatedByFunction.CollectionParameter<int>("customerIds");
             areRatedByFunction.Namespace = "AccountOpening.Functions";*/
           
            builder.Singleton<Customer> ("Wambui");
            return builder.GetEdmModel();
        }
    }
}
