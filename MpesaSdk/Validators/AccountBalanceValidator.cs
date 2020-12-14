﻿using FluentValidation;
using MpesaSdk.Dtos;
using System;

namespace MpesaSdk.Validators
{
    public class AccountBalanceValidator : AbstractValidator<AccountBalance>
    {
        public AccountBalanceValidator()
        {
            int i = 0;
            RuleFor(x => x.PartyA).NotNull().Must(x => int.TryParse(x, out i)).Length(5, 7);
            RuleFor(x => x.IdentifierType).NotNull().NotEmpty();
            RuleFor(x => x.Remarks).NotNull().NotEmpty();
            RuleFor(x => x.Initiator).NotNull().NotEmpty();
            RuleFor(x => x.QueueTimeOutURL).NotNull().Must(x => LinkMustBeAUri(x));
            RuleFor(x => x.ResultURL).NotNull().Must(x => LinkMustBeAUri(x));
        }

        private static bool LinkMustBeAUri(string link)
        {
            if (!Uri.IsWellFormedUriString(link, UriKind.Absolute))
            {
                return false;
            }
            return true;
        }
    }
}
