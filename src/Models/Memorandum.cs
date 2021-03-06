using System;
using FluentValidation;
using Raven.Client;
using Raven.Client.Linq;
using System.Linq;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using GestUAB.Validators;
using GestUAB.Models;

namespace GestUAB
{
    /// <summary>
    /// Memorandum Class
    /// </summary>
    public class Memorandum : IModel
    {
        #region EnumType
        [GlobalizedEnum(typeof(MemorandumType), "Geral", "Diária")]
        public enum MemorandumType {
            General,
            DailyRate
        }
        #endregion

        #region Builder

        /// <summary>
        /// Builder Class Memorandum
        /// </summary>
        public Memorandum ()
        {

        }

        /// <summary>
        /// Static method that creates a default Memorandum.
        /// </summary>
        /// <returns> Default Memorandum </returns>
        /// 
        public static Memorandum DefaultMemorandum()
        {
            return new Memorandum() { 
                Id = Guid.NewGuid(),
                Observation = string.Empty,
                Destiny = string.Empty,
                StartDate = string.Empty,
                FinishDate = string.Empty,
                RequesterName = string.Empty,
                BankAccount = string.Empty,
                CovenantNumber = string.Empty,
                Type = 0
            };
        }
        #endregion

        #region IModel implementation
        [Display(Name = "Código",
                 Description= "Código do memorando.")]
        [ScaffoldVisibility(all:ScaffoldVisibilityType.Hidden)] 
        public System.Guid Id { get ; set ; }
        #endregion

        #region Variables
        [Display(Name = "Referente",
                 Description= "Texto \"Referente\" a.")]
        [ScaffoldVisibility(all:ScaffoldVisibilityType.Show)] 
        public String Observation { get ; set ; }

        [Display(Name = "Destino",
                 Description= "Local de destino.")]
        [ScaffoldVisibility(all:ScaffoldVisibilityType.Show)] 
        public String Destiny { get ; set ; }

        [Display(Name = "Data Ida",
                 Description= "Data de ida.")]
        [ScaffoldVisibility(all:ScaffoldVisibilityType.Show)] 
        public String StartDate { get ; set ; }

        [Display(Name = "Data Volta",
                 Description= "Data de volta.")]
        [ScaffoldVisibility(all:ScaffoldVisibilityType.Show)] 
        public String FinishDate { get ; set ; }

        [Display(Name = "Solicitante",
                 Description= "Nome do solicitante.")]
        [ScaffoldVisibility(all:ScaffoldVisibilityType.Show)] 
        public String RequesterName { get ; set ; }

        [Display(Name = "Conta Bancaria",
                 Description= "Conta para deposito.")]
        [ScaffoldVisibility(all:ScaffoldVisibilityType.Show)] 
        public String BankAccount { get ; set ; }

        [Display(Name = "Convenio",
                 Description= "Numero do convenio.")]
        [ScaffoldVisibility(all:ScaffoldVisibilityType.Show)] 
        public string CovenantNumber { get ; set ; }

        [Display(Name = "Diaria solicitada",
                 Description= "Tipo de diaria solicitada.")]
        [ScaffoldVisibility(all:ScaffoldVisibilityType.Show)] 
        [ScaffoldSelectProperties("", SelectType.Single)]
        public MemorandumType Type { get ; set ; }
        #endregion
    }

    /// <summary>
    /// Memorandum Validator
    /// </summary>
    /// 
    public class MemorandumValidator : ValidatorBase<Memorandum>
    {
        /// <summary>
        /// Method that validates when the object will be created or changed.
        /// </summary>
        /// 
        public MemorandumValidator ()
        {
            using (var session = DocumentSession) {
                RuleFor (memorandum => memorandum.RequesterName)
                    .NotEmpty ().WithMessage ("O nome do requerente é obrigatório.")
                    .Length (5, 50).WithMessage ("O nome do requerente deve conter entre 5 e 50 caracteres.")
                        .Matches (@"^[a-zA-Z][a-zA-Z0-9_]*\.?[a-zA-Z0-9_]*$").WithMessage ("Insira somente letras.");
                RuleFor (memorandum => memorandum.Observation)
                    .NotEmpty ().WithMessage ("A observaçao é obrigatório.");
                RuleFor (memorandum => memorandum.Destiny)
                    .NotEmpty ().WithMessage ("O destino é obrigatório.");
            }
            RuleSet ("Update", () => {
                RuleFor (memorandum => memorandum.RequesterName)
                    .NotEmpty ().WithMessage ("O nome do requerente é obrigatório.")
                    .Length (5, 50).WithMessage ("O nome do requerente deve conter entre 5 e 50 caracteres.")
                        .Matches (@"^[a-zA-Z][a-zA-Z0-9_]*\.?[a-zA-Z0-9_]*$").WithMessage ("Insira somente letras.");
                RuleFor (memorandum => memorandum.Observation)
                    .NotEmpty ().WithMessage ("A observaçao é obrigatório.");
                RuleFor (memorandum => memorandum.Destiny)
                    .NotEmpty ().WithMessage ("O destino é obrigatório.");
            }
            );
        }
    }
}

