using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication.Controllers
{
    public class PersonasController : Controller
    {
        public ActionResult Lista()
        {
            Consigna1Entities ctx = new Consigna1Entities();
            List<Persona> listaPersonas = new List<Persona>();
            listaPersonas = (from persona in ctx.Persona
                             join direccion in ctx.Direccion on persona.direccion equals direccion.idDireccion
                             select persona).ToList();
            ViewData.Model = listaPersonas;
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Crear(Persona persona, Direccion direccion)
        {
            Consigna1Entities ctx = new Consigna1Entities();
            ctx.Direccion.Add(direccion);
            persona.direccion = direccion.idDireccion;
            ctx.Persona.Add(persona);
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction("Lista");
        }

        public ActionResult Modificar(int id)
        {
            Consigna1Entities ctx = new Consigna1Entities();
            Persona persona = ctx.Persona.FirstOrDefault(o => o.idPersona == id);
            ViewData.Model = persona;
            return View();
        }

        [HttpPost]
        public ActionResult Modificar(Persona personaNueva, Direccion direccionNueva)
        {
            Consigna1Entities ctx = new Consigna1Entities();
            Persona personaAnterior         = ctx.Persona.FirstOrDefault(o => o.idPersona == personaNueva.idPersona);
            Direccion direccionAnterior     = ctx.Direccion.FirstOrDefault(o => o.idDireccion == direccionNueva.idDireccion);
            personaAnterior.nombre          = personaNueva.nombre;
            personaAnterior.apellido        = personaNueva.apellido;
            personaAnterior.numeroDocumento = personaNueva.numeroDocumento;
            personaAnterior.fechaNacimiento = personaNueva.fechaNacimiento;
            direccionAnterior.calle         = direccionNueva.calle;
            direccionAnterior.numero        = direccionNueva.numero;
            try
            {
                ctx.SaveChanges();
            }
            catch (Exception)
            {
                return RedirectToAction("Error");
            }
            return RedirectToAction("Lista");
        }

        public ActionResult Borrar(int id)
        {
            Consigna1Entities ctx = new Consigna1Entities();
            Persona persona     = ctx.Persona.FirstOrDefault(o => o.idPersona == id);
            Direccion direccion = ctx.Direccion.FirstOrDefault(o => o.idDireccion == persona.direccion);
            ctx.Persona.Remove(persona);
            ctx.Direccion.Remove(direccion);
            ctx.SaveChanges();
            return RedirectToAction("Lista");
        }
    }
}