namespace TodoApp.API.Exceptions;

public class DbContextNullException(string context) : ArgumentNullException($"'{context}' has not been injected correctly.");