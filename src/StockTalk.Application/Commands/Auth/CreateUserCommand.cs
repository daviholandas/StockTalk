﻿using Ardalis.Result;
using MediatR;

namespace StockTalk.Application.Commands.Auth;

public record struct CreateUserCommand(
    string Email,
    string Password) :
    IRequest<Result>;