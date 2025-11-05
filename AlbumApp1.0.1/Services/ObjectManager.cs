using AlbumApp1._0._1.Collections;
using AlbumApp1._0._1.Interfaces;
using AlbumApp1._0._1.Models.Tables;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlbumApp1._0._1.Services
{
    public partial class ObjectManager : IObjectManager
    {
        private Пользователи Users;
        private Гости Guests;
        private Фотографии Photos;
        private Объекты Objects;
        private Места Places;
        private Оборудование Accessories;
        private Стили Styles;
        private SynchronizedObservableCollection<Фотографии> PhCol;
        public ObjectManager()

        {
            if (Users == null)
            {
                Users = new();
                 if (Guests == null)
                {
                    Guests = new();
                    if (Photos==null)
                    {
                        Photos = new();
                        if (Objects == null)
                        {
                            Objects = new();
                            if (Places == null)
                            {
                                Places = new();
                                if (Accessories == null)
                                {
                                    Accessories = new();
                                    if (Styles == null)
                                    {
                                        Styles = new();
                                    }
                                }

                            }
                        }
                    }
                }
            }
          
        }
        public object TakeObject<T>(T value) where T : class 
        {

            if (Users.GetType() == typeof(T))
            {
                var stringName = Users.GetType().Name;
                if (stringName == typeof(T).Name)
                {
                    
                    if (Users == null)
                    {
                        return Users = new();
                    }
                    else
                    {
                        return Users;
                    }
                }
                else
                {
                    return null;
                }
            }
            else if (Guests.GetType() == typeof(T))
            {
                var stringName = Guests.GetType().Name;
                if (stringName == typeof(T).Name)
                {
                    if (Guests == null)
                    {
                        return Guests = new();
                    }
                    else
                    {
                        return Guests;
                    }
                }
                else
                {
                    return null;
                }
            }
            else if (Photos.GetType() == typeof(T))
            {
                var stringName = Photos.GetType().Name;
                if (stringName == typeof(T).Name)
                {
                    if (Photos == null)
                    {
                        return Photos = new();
                    }
                    else
                    {
                        return Photos;
                    }
                }
                else
                {
                    return null;
                }
            }
            else if (Objects.GetType() == typeof(T))
            {
                var stringName = Objects.GetType().Name;
                if (stringName == typeof(T).Name)
                {
                    if (Objects == null)
                    {
                        return Objects = new();
                    }
                    else
                    {
                        return Objects;
                    }
                }
                else
                {
                    return null;
                }
            }
            else if (Places.GetType() == typeof(T))
            {
                var stringName = Places.GetType().Name;
                if (stringName == typeof(T).Name)
                {
                    if (Places == null)
                    {
                        return Places = new();
                    }
                    else
                    {
                        return Places;
                    }
                }
                else
                {
                    return null;
                }
            }
            else if (Accessories.GetType() == typeof(T))
            {
                var stringName = Accessories.GetType().Name;
                if (stringName == typeof(T).Name)
                {
                    if (Accessories == null)
                    {
                        return Accessories = new();
                    }
                    else
                    {
                        return Accessories;
                    }
                }
                else
                {
                    return null;
                }
            }
            else if (Styles.GetType() == typeof(T))
            {
                var stringName = Styles.GetType().Name;
                if (stringName == typeof(T).Name)
                {
                    if (Styles == null)
                    {
                        return Styles = new();
                    }
                    else
                    {
                        return Styles;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

    }
}
