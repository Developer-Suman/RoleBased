using Algorithm.Models;
using Microsoft.ML;
using RoleBased.Models.Domain;
using RoleBased.Services.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
    public class Similarities
    {
        List<AlgoProduct> _products;
        MLContext _mlContext;
        
        //AlgoProduct _productsToRecommedFor;

        public Similarities(List<Material> materials)
        {
            
            //Create ML Context
            _mlContext = new MLContext();
          
            _products = materials.Select(p => new AlgoProduct()
            {
               
                Id = p.materialsId,
                materialName = p.materialName

            }).ToList();


        }


        public List<int> GetSimilarProducts(int materialsId, double similarityThreshold = 0.3)
        {
            //Create the IDataView from the product Data
            var dataView = _mlContext.Data.LoadFromEnumerable(_products.Select(p => new AlgoProduct()
            {
                materialName = p.materialName

            }));

            //Define a data preparation pipeline for description
            var descriptionPipeline = _mlContext.Transforms.Text.FeaturizeText("materialName", "materialName")
                .Append(_mlContext.Transforms.NormalizeMinMax("materialName"));

            //Fit the description data preparation pipeline 
            var descriptionpreprocessedData = descriptionPipeline.Fit(dataView).Transform(dataView);

            //Extract TF-IDF vectors for description
            var descriptionTfidfVectors = _mlContext.Data.CreateEnumerable<TfidfTransformedData>(descriptionpreprocessedData, reuseRowObject: false);


            //Update the product with TF-IDF vectors for description
            int i = 0;
            foreach(var product in _products)
            {
                product.CombinedTFIDVector = descriptionTfidfVectors.ElementAt(i++).materialName;
            }

            var _productToRecommendFor = _products.FirstOrDefault(p=>p.Id == materialsId);

            //Recommend for product
            if( _productToRecommendFor != null )
            {
                var recommendedProducts = _products
                    .Where(p => p.Id != _productToRecommendFor.Id)
                    .Select(p => new
                    {
                        Product = p,
                        Similarity = CalculateSimilarity(_productToRecommendFor.CombinedTFIDVector, p.CombinedTFIDVector)
                    })
                    .Where(r => r.Similarity >= similarityThreshold)
                    .OrderByDescending(r => r.Similarity)
                    .Select(r => r.Product);



                return recommendedProducts
                    .Select(p => p.Id)
                    .Cast<int>()   //Explicitly cast to IEnumerable<int>
                    .ToList();
            }
            return new List<int>();




        }


        public double CalculateSimilarity(float[] vector1, float[] vector2)
        {
            if(vector1.Length != vector2.Length)
            {
                throw new ArgumentException("Vector must have the same length");
            }

            double dotProduct = 0;
            double magnitude1 = 0;
            double magnitude2 = 0;


            for(int i=0; i<vector1.Length; i++)
            {
                dotProduct += vector1[i] * vector2[i];
                magnitude1 += Math.Pow(vector1[i], 2);
                magnitude2 += Math.Pow(vector2[i], 2);
            }

            magnitude1  = Math.Sqrt(magnitude1);
            magnitude2 = Math.Sqrt(magnitude2);


            if(magnitude1 ==0 || magnitude2 == 0)
            {
                return 0.0;  //handle division by zero
            }

            return dotProduct/(magnitude1 * magnitude2);
        }
    }
}
