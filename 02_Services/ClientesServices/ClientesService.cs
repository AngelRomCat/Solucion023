using _04_Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Services.ClientesServices
{
    public class ClientesService
    {
        private static NorthWindTuneadoDbContext _db = null;
        public ClientesService()
        {
            if (_db == null)
            {
                _db = new NorthWindTuneadoDbContext();
            }
        }

        //Index
        public IList<Cliente> List(int? id)
        {
            IList<Cliente> clientes = null;
            if (id == null || id < 1)
            {
                clientes = _db.Cliente.ToList();
            }
            else
            {
                clientes = _db.Cliente
                                .Where(x => x.CustomerID == id)
                                .ToList();
            }

            return clientes;
        }



        //Details
        public Cliente Detail(int id)
        {
            Cliente cliente = null;
            cliente = _db.Cliente
                                .Where(x => x.CustomerID == id)
                                .FirstOrDefault();
            return cliente;
        }
        //Create
        public bool Create(Cliente cliente)
        {
            bool ok = false;
            try
            {
                _db.Cliente.Add(cliente);
                ok = SaveChanges();
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //Edit
        public bool Edit(Cliente cliente)
        {
            bool ok = false;
            try
            {

                Cliente buscada = _db.Cliente
                                    .Where(x => x.CustomerID == cliente.CustomerID)
                                    .FirstOrDefault();

                buscada.CustomerName = cliente.CustomerName;
                buscada.ContactName = cliente.ContactName;
                buscada.City = cliente.City;
                buscada.Address = cliente.Address;
                buscada.PostalCode = cliente.PostalCode;
                buscada.Country = cliente.Country;

                //Guardamos cambios:
                ok = SaveChanges();
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //Delete
        public bool Delete(Cliente cliente)
        {
            bool ok = false;
            try
            {
                _db.Cliente.Remove(cliente);
                //Guardamos cambios:
                ok = SaveChanges();
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //SaveChanges
        public bool SaveChanges()
        {
            bool ok = false;
            try
            {
                int retorno = 0;
                retorno = _db.SaveChanges();
                if (retorno > 0)
                {
                    ok = true;
                }
            }
            catch (Exception e)
            {
                //Log
                //throw;
            }

            return ok;
        }
        //Dispose
        public bool Dispose(bool ok)
        {
            if (ok == true)
            {
                _db.Dispose();
            }

            return ok;
        }




    }
}
