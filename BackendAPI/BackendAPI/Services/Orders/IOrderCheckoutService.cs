using System.Security.Claims;
using ShareView.DTO;

namespace BackendAPI.Services.Orders;

public interface IOrderCheckoutService
{
    Task<CheckoutOrderResponseDTO> CheckoutAsync(
        CheckoutOrderRequestDTO request,
        ClaimsPrincipal user,
        CancellationToken cancellationToken);
}
