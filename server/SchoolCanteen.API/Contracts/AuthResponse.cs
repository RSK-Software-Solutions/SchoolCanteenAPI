﻿namespace SchoolCanteen.API.Contracts;

public record AuthResponse(string Email, string UserName, string Token);
