using Common.Basic.Blocks;
using Common.Basic.Collections;
using Common.Basic.DDD;
using Common.Basic.Repository;

namespace PageTree.App.UseCases.Common
{
    public static class EditableListAndRepositoryExtensions_Create
    {
        private static string NewID => Guid.NewGuid().ToString();

        public static async Task<(TOwner, TChild)> GetOwnerAndChildOrCreateChild<TOwner, TChild>(
           this IRepository<TOwner> ownerRepository, IRepository<TChild> childRepository, string ownerID, string ownerChildIDPropertyName, Result result)
           where TOwner : Entity
           where TChild : Entity
        {
            TOwner owner = await ownerRepository.Get(ownerID, result);
            var ownerType = typeof(TOwner);
            var ownerChildIDProperty = ownerType.GetProperty(ownerChildIDPropertyName);

            string childID = ownerChildIDProperty.GetValue(owner) as string;

            TChild item = default;
            if (childID.IsNullOrEmpty())
            {
                item = await childRepository.Create(result);
            }
            else
            {
                item = await childRepository.Get(childID, result);
                if (item)
                    return (owner, item);

                item = await childRepository.Create(result);
            }

            ownerChildIDProperty.SetValue(owner, item.ID);
            await ownerRepository.Save(owner);

            return (owner, item);
        }

        public static async Task<T> Create<T>(this IRepository<T> repository, Result result)
        {
            T item = (T)Activator.CreateInstance(typeof(T), NewID);

            await repository.Save(item, result);

            return item;
        }
    }
}
