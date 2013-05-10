using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace DevelopmentWithADot.EntityFrameworkFluentValidation
{
	public static class DbContextExtensions
	{
		private static IDictionary<Type, Tuple<Delegate, String>> entityValidations = new ConcurrentDictionary<Type, Tuple<Delegate, String>>();

		public static void AddEntityValidation<TContext, TEntity>(this TContext context, Expression<Func<TContext, IDbSet<TEntity>>> collection, Func<TEntity, Boolean> validation, String message) where TContext : DbContext where TEntity : class
		{
			AddEntityValidation<TEntity>(context, validation, message);
		}

		public static void AddEntityValidation<TEntity>(this DbContext context, Func<TEntity, Boolean> validation, String message) where TEntity : class
		{
			if (context == null)
			{
				throw new ArgumentNullException("context");
			}

			if (validation == null)
			{
				throw new ArgumentNullException("validation");
			}

			if (String.IsNullOrWhiteSpace(message) == true)
			{
				throw new ArgumentNullException("message");
			}

			if (entityValidations.ContainsKey(typeof(TEntity)) == false)
			{
				(context as IObjectContextAdapter).ObjectContext.SavingChanges += delegate
				{
					if (context.Configuration.ValidateOnSaveEnabled == true)
					{
						IEnumerable<TEntity> entities = context.ChangeTracker.Entries<TEntity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified).Select(x => x.Entity).ToList();

						foreach (TEntity entity in entities)
						{
							String error = ValidateEntity(entity);

							if (String.IsNullOrWhiteSpace(error) == false)
							{
								throw (new ValidationException(error));
							}
						}
					}
				};
			}

			entityValidations[typeof(TEntity)] = new Tuple<Delegate, String>(validation, message);
		}

		private static String ValidateEntity<TEntity>(TEntity entity)
		{
			Type entityType = typeof(TEntity);

			if (entityValidations.ContainsKey(entityType) == true)
			{
				Tuple<Delegate, String> entry = entityValidations[entityType];
				Func<TEntity, Boolean> validation = entry.Item1 as Func<TEntity, Boolean>;

				if (validation(entity) == false)
				{
					return (entry.Item2);
				}
			}

			return (null);
		}
	}
}
