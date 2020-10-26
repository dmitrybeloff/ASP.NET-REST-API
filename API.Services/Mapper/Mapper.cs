using API.Domains.Interfaces;
using API.Services.Mapper.Interaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace API.Services.Mapper
{
    /// <inheritdoc/>
    public class Mapper : IMapper
    {
        /// <inheritdoc/>
        public TDest MapEntityToModel<TSource, TDest>(TSource input, HashSet<string> existingNames = null)
            where TSource : class, IEntity
            where TDest : class, IDataTransferObject
        {
            if (input == null)
                return default;

            existingNames ??= new HashSet<string>();
            existingNames.Add(typeof(TSource).Name);

            var sourceProperties = typeof(TSource).GetProperties();
            var destinationProperties = typeof(TDest).GetProperties();

            var destination = (TDest)Activator.CreateInstance(typeof(TDest));

            for (var i = 0; i < sourceProperties.Length; i++)
            {
                var sourcePropValue = sourceProperties[i].GetValue(input);

                if (sourcePropValue != null && !existingNames.Contains(sourceProperties[i].Name))
                {
                    // Process Many-To-Many relashionship
                    if (CheckIfObjectTypeCanBeList(sourceProperties[i].PropertyType)
                        && typeof(IManyToManyRelationshipMember).IsAssignableFrom(sourceProperties[i].PropertyType.GenericTypeArguments[0]))
                    {
                        existingNames.Add(sourceProperties[i].Name);
                        var destinationProperty = destinationProperties.FirstOrDefault(x => x.Name == sourceProperties[i].Name);

                        if (destinationProperty != null)
                        {
                            IList newListValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(destinationProperty.PropertyType.GenericTypeArguments[0]));

                            foreach (var item in sourceProperties[i].GetValue(input) as IList)
                            {
                                var itemProperties = item.GetType().GetProperties();

                                foreach (var itemProperty in itemProperties)
                                {
                                    if (!existingNames.Contains(itemProperty.Name)
                                        && typeof(IEntity).IsAssignableFrom(itemProperty.PropertyType))
                                    {
                                        var newPropValue = GetType().GetMethod("MapEntityToModel")
                                            .MakeGenericMethod(itemProperty.PropertyType, destinationProperty.PropertyType.GenericTypeArguments[0])
                                            .Invoke(this, new object[] { itemProperty.GetValue(item), new HashSet<string>(existingNames) });

                                        newListValues.Add(newPropValue);
                                    }
                                }

                                destinationProperty.SetValue(destination, newListValues);
                            }
                        }
                    }
                    else
                    {
                        for (var j = 0; j < destinationProperties.Length; j++)
                        {
                            if (sourceProperties[i].Name == destinationProperties[j].Name)
                            {
                                // Process One-To-Many realashionship
                                if (CheckIfObjectTypeCanBeList(sourceProperties[i].PropertyType) && CheckIfObjectTypeCanBeList(destinationProperties[j].PropertyType))
                                {
                                    var newListValues = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(destinationProperties[i].PropertyType.GenericTypeArguments[0]));
                                    foreach (var item in sourcePropValue as IList)
                                    {
                                        var newPropValue = GetType().GetMethod("MapEntityToModel")
                                            .MakeGenericMethod(sourceProperties[i].PropertyType.GenericTypeArguments[0], destinationProperties[i].PropertyType.GenericTypeArguments[0])
                                            .Invoke(this, new object[] { item, new HashSet<string>(existingNames) });

                                        newListValues.Add(newPropValue);
                                    }

                                    destinationProperties[j].SetValue(destination, newListValues);
                                }
                                // Process One-To-One realashionship
                                else if (typeof(IEntity).IsAssignableFrom(sourceProperties[i].PropertyType)
                                    && typeof(IDataTransferObject).IsAssignableFrom(destinationProperties[i].PropertyType))
                                {
                                    var newPropValue = GetType().GetMethod("MapEntityToModel")
                                        .MakeGenericMethod(sourceProperties[i].PropertyType, destinationProperties[i].PropertyType)
                                        .Invoke(this, new object[] { sourcePropValue, new HashSet<string>(existingNames) });

                                    destinationProperties[j].SetValue(destination, newPropValue);
                                }
                                // Map Value
                                else
                                {
                                    destinationProperties[j].SetValue(destination, sourcePropValue);
                                }

                                break;
                            }
                        }
                    }
                }
            }

            return destination;
        }

        private bool CheckIfObjectTypeCanBeList(Type objectType)
        {
            if (objectType.IsGenericType)
            {
                var genericType = objectType.GenericTypeArguments[0];
                var listType = typeof(List<>).MakeGenericType(genericType);

                return objectType.IsAssignableFrom(listType);
            }

            return false;
        }            
    }
}
