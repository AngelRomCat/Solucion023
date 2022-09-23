using _04_Data.Data;
using _04_Data.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _02_Services.EmpleadosServices
{
    public class EmpleadosService
    {
        private static NorthWindTuneadoDbContext _db = null;
        public EmpleadosService()
        {
            if (_db == null)
            {
                _db = new NorthWindTuneadoDbContext();
            }
        }

        //Index
        public IList<Empleado> List(int? id)
        {
            IList<Empleado> empleados = null;
            if (id == null || id < 1)
            {
                empleados = _db.Empleado.ToList();
            }
            else
            {
                empleados = _db.Empleado
                                .Where(x => x.EmployeeID == id)
                                .ToList();
            }

            return empleados;
        }



        //Details
        public Empleado Detail(int id)
        {
            Empleado empleado = null;
            empleado = _db.Empleado
                                .Where(x => x.EmployeeID == id)
                                .FirstOrDefault();
            return empleado;
        }
        //Create
        public bool Create(Empleado empleado)
        {
            bool ok = false;
            try
            {
                _db.Empleado.Add(empleado);
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
        public bool Edit(EmpleadoViewModel viewModel)
        {
            bool ok = false;
            try
            {
                DateTime? birthDate = null;
                if (viewModel != null && viewModel.birthDate != null && viewModel.birthDate != "")
                {
                    if (DateTime.TryParse(viewModel.birthDate, out DateTime result) == true)
                    {
                        birthDate = result;
                    }
                }
                Empleado buscada = _db.Empleado
                                    .Where(x => x.EmployeeID == viewModel.EmployeeID)
                                    .FirstOrDefault();

                buscada.EmployeeID = viewModel.EmployeeID;
                buscada.FirstName = viewModel.FirstName;
                buscada.LastName = viewModel.LastName;
                buscada.birthDate = birthDate;
                buscada.Photo = viewModel.Photo;
                buscada.Notes = viewModel.Notes;


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
        public bool Delete(Empleado empleado)
        {
            bool ok = false;
            try
            {
                _db.Empleado.Remove(empleado);
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
