using BroomWorks.Rental.Domain;
using BroomWorks.Rental.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace BroomWorks.Rental.Business.Services.Implementation;

public abstract class CleanService
{
    private readonly IReservationService _reservationService;
    private readonly IBroomService _broomService;
    private readonly ICustomerService _customerService;
    private readonly IApplicationContext _applicationContext;
    private readonly IArchiveService _archiveService;
    private readonly ILogger<CleanService> _logger;

    public CleanService(
        IReservationService reservationService,
        IBroomService broomService,
        ICustomerService customerService,
        IApplicationContext applicationContext,
        IArchiveService archiveService,
        ILogger<CleanService> logger)
    {
        _reservationService = reservationService;
        _broomService = broomService;
        _customerService = customerService;
        _applicationContext = applicationContext;
        _archiveService = archiveService;
        _logger = logger;
    }


    #region guards
    // 1
    // funktsioon võiks lõppeda returniga

    public bool IsCustomerBirthday1(Guid customerId)
    {
        var customer = _customerService.GetCustomerAsync(customerId).Result;
        if (customer == null)
        {
            throw new Exception("klient on kadunud");
        }

        var now = _applicationContext.GetCurrentTime();
        if (customer.DateOfBirth.Month == now.Month && customer.DateOfBirth.Day == now.Day)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsCustomerBirthday2(Guid customerId)
    {
        var customer = _customerService.GetCustomerAsync(customerId).Result;

        if (customer != null)
        {
            return IsCustomerBirthday2(customer);
        }
        else
        {
            throw new Exception("klient on kadunud");
        }
    }

    public bool IsCustomerBirthday2(Customer customer)
    {
        var now = _applicationContext.GetCurrentTime();
        if (customer.DateOfBirth.Month == now.Month && customer.DateOfBirth.Day == now.Day)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region comments
    // 2
    // commentid tekitavad müra
    // commentid lähevad out-of-sync koodist

    public class CustomerExample
    {
        // Id of Customer
        public Guid Id { get; set; }
        // Name of Customer
        public string Name { get; set; }
        // Birthday of Customer
        public DateTimeOffset DateOfBirth { get; set; }
    }

    /// <summary>
    /// Deletes broom
    /// </summary>
    /// <param name="broomId">Id of broom</param>
    public abstract void DeleteBroom(Guid broomId);


    // miks see kood seal vahel on?
    // miks teda enam pole?
    // miks seda alles hoitakse, äkki on vaja?
    // kas koodiga on mingi task seotud?
    // kas autor töötab ikka firmas, äkki mäletab?

    public decimal CalculateProfit(decimal totalSalaryCost)
    {
        var reservations = _reservationService.GetReservationsAsync().Result;

        // reservations = reservations.Where(r => r.Type == "LongTerm").ToArray();
        // _logger.LogInformation($"{reservations.Length} bronni arvutuses");

        var totalCost = reservations.Select(GetReservationCost).Sum();

        return totalCost - totalSalaryCost;
    }

    // Quake 3
    // ~4 korda kiirem kui loetav kood
    float q_rsqrt(float number)
    {
        unsafe
        {
            long i;
            float x2, y;
            const float threehalfs = 1.5F;

            x2 = number * 0.5F;
            y = number;
            i = *(long*)&y;                       // evil floating point bit level hacking
            i = 0x5f3759df - (i >> 1);               // what the fuck?
            y = *(float*)&i;
            y = y * (threehalfs - (x2 * y * y));   // 1st iteration
                                                   // y  = y * ( threehalfs - ( x2 * y * y ) );   // 2nd iteration, this can be removed
            return y;
        }
    }
    #endregion

    #region taasväärtustamine
    // 3
    // alati algväärtusta
    // ära kunagi taasväärtusta

    public Guid? WhoHasBroom1(Guid broomId)
    {
        var reservations = _reservationService.GetReservationsAsync().Result;

        Customer? customer = null;

        foreach (var reservation in reservations)
        {
            if (reservation.Broom.Id == broomId)
            {
                customer = reservation.Customer;
            }
        }

        // palju koodi
        // keegi arvutab customerile uue väärtuse
        // immutable variables
        // palju koodi

        return customer?.Id;
    }

    public Guid? WhoHasBroom2(Guid broomId)
    {
        var reservations = _reservationService.GetReservationsAsync().Result;

        Customer? customer = reservations
            .FirstOrDefault(r => r.Broom.Id == broomId)
            ?.Customer;

        return customer?.Id;
    }
    #endregion

    #region nimetamine
    // 4    

    // koodi loogikat pole mõtet kommenteerida
    // vihjab puuduvale meetodile

    public void DeleteAllEndedReservations1()
    {
        var reservations = _reservationService.GetReservationsAsync().Result;

        foreach (var reservation in reservations)
        {
            // reservation on aktiivne
            if (reservation.End != null)
            {
                _reservationService.DeleteAsync(reservation.Id).Wait();
            }
        }
    }

    public void DeleteAllEndedReservations2()
    {
        var reservations = _reservationService.GetReservationsAsync().Result;

        foreach (var reservation in reservations)
        {
            if (IsReservationEnded(reservation))
            {
                _reservationService.DeleteAsync(reservation.Id).Wait();
            }
        }
    }

    private bool IsReservationEnded(Reservation reservation)
    {
        return reservation.End != null;
    }

    // muutuja nime pikkus võiks peegeldada skoopi

    public Broom[] GetAllActivelyReservedBrooms1()
    {
        var brooms = _broomService.GetBroomsAsync().Result;
        var reservations = _reservationService.GetReservationsAsync().Result;

        var activeReservations = reservations.Where(reservation => reservation.End == null);

        var reservedBrooms = brooms
            .Join(activeReservations, broom => broom.Id, reservation => reservation.Id, (broom, reservation) => broom)
            .ToArray();

        return reservedBrooms;
    }

    public Broom[] GetAllActivelyReservedBrooms2()
    {
        var brooms = _broomService.GetBroomsAsync().Result;
        var reservations = _reservationService.GetReservationsAsync().Result;

        var activeReservations = reservations.Where(r => r.End == null);

        var reservedBrooms = brooms
            .Join(reservations, b => b.Id, r => r.Id, (b, r) => b)
            .ToArray();

        return reservedBrooms;
    }

    // meetodi nimi võiks peegeldada sisu
    // kunagi kustutati valimatult kõike
    // tihti uuendatakse meetodit, aga mitte kommentaari
    // parem on üldse mitte kommenteerida ja kohe ilus nimi panna

    public void DeleteAllReservations()
    {
        var reservations = _reservationService.GetReservationsAsync().Result;

        foreach (var reservation in reservations)
        {
            if (reservation.End != null)
            {
                _reservationService.DeleteAsync(reservation.Id).Wait();
            }
        }
    }

    public abstract void DeleteAllEndedReservations3();

    #endregion

    #region muutujaskoop
    // 5

    // muutujad võiksid kasutamisele võimalikult lähedal deklareeritud olla
    // mitut mõtet segamini

    public decimal CalculateInvoiceAmount1(Guid customerId)
    {
        var reservations = _reservationService.GetReservationsAsync().Result;

        var customer = _customerService.GetCustomerAsync(customerId).Result;

        var endedReservations = reservations.Where(IsReservationEnded);

        var discount = _reservationService.GetDiscountForBirthdayAsync(customer).Result;

        var customerEndedReservations = endedReservations.Where(r => r.Customer.Id == customer.Id);

        var totalAmount = customerEndedReservations.Select(GetReservationCost).Sum();
        var totalDiscountedAmount = totalAmount * discount;

        return totalDiscountedAmount;
    }

    public decimal CalculateInvoiceAmount2(Guid customerId)
    {
        var reservations = _reservationService.GetReservationsAsync().Result;
        var endedReservations = reservations.Where(IsReservationEnded);
        var customerEndedReservations = endedReservations.Where(r => r.Customer.Id == customerId);

        var totalAmount = customerEndedReservations.Select(GetReservationCost).Sum();

        var customer = _customerService.GetCustomerAsync(customerId).Result;
        var discount = _reservationService.GetDiscountForBirthdayAsync(customer).Result;
        var totalDiscountedAmount = totalAmount * discount;

        return totalDiscountedAmount;
    }

    public abstract decimal GetReservationCost(Reservation reservation);

    #endregion

    #region kiss
    // 6
    // ärme ole kavalad
    // teised arendajad ei ole nii targad
    // pole klienti pole arvet

    public decimal GetCustomerDiscount1(Guid customerId)
    {
        var customer = _customerService.GetCustomerAsync(customerId).Result;

        return customer == null ? 0m : IsCustomerBirthday1(customerId) ? 0.5m : 1m;
    }

    public decimal GetCustomerDiscount2(Guid customerId)
    {
        var customer = _customerService.GetCustomerAsync(customerId).Result;

        if (customer == null)
        {
            return 0m;
        }
        else
        {
            return GetCustomerDiscount2(customer);
        }
    }

    public abstract decimal GetCustomerDiscount2(Customer customer);

    #endregion

    #region ifkontrollid
    // 7
    // kontrollid eraldi meetodisse
    // hea on boolean meetodid vormistada küsimusteks is/has

    public bool IsCustomerEligibleForCampaign1(Customer customer)
    {
        var now = _applicationContext.GetCurrentTime();
        var customerReservations = _reservationService.GetReservationsForCustomerAsync(customer.Id).Result;

        if ((now.Year - customer.DateOfBirth.Year > 18)
            && customer.ActivatedBy != null
            && customerReservations.Any())
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool IsCustomerEligibleForCampaign2(Customer customer)
    {
        if (IsCustomerAdult(customer)
            && IsCustomerActive(customer)
            && HasCustomerReservations(customer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool IsCustomerAdult(Customer customer)
    {
        var now = _applicationContext.GetCurrentTime();
        return now.Year - customer.DateOfBirth.Year > 18;
    }

    private bool IsCustomerActive(Customer customer)
    {
        return customer.ActivatedBy != null;
    }

    private bool HasCustomerReservations(Customer customer)
    {
        var customerReservations = _reservationService.GetReservationsForCustomerAsync(customer.Id).Result;
        return customerReservations.Any();
    }

    #endregion

    #region nullid
    // 8
    // try pattern
    // exceptionid on kallid
    // vahest unustame ära..

    // krahhib või annab kliendi
    public abstract Customer GetCustomer(Guid customerId);

    // ei krahhi
    public abstract bool TryGetCustomer(Guid customerId, out Customer customer);

    public void ActivateCustomer1(Guid customerId)
    {
        var customer = GetCustomer(customerId);
        customer.IsActive = true;
    }

    public void ActivateCustomer2(Guid customerId)
    {
        if (TryGetCustomer(customerId, out var customer))
        {
            customer.IsActive = true;
        }
    }

    // libe tee
    // try-get on gateway drug
    // Result<TSuccess, TFailure>
    // Option<T>
    // Railway pattern
    // partial application
    // monaadid F#/Clojure/Haskell

    #endregion

    #region tavad
    // 9
    // järgi olemasolevat stiili
    // tee igal pool täpselt sama valesti
    // sama mure arhitektuuriga

    public string BuildCustomerReport1(Guid CustomerId)
    {
        var Customer = _customerService.GetCustomerAsync(CustomerId).Result;
        var Reservations = _reservationService.GetReservationsForCustomerAsync(CustomerId).Result;

        // keegi leidis vea ja tegi paranduse
        var ended_reservations = Reservations.Where(IsReservationEnded).ToArray();
        Reservations = ended_reservations; // vastik taasväärtustamine

        var TotalSum = Reservations.Select(GetReservationCost).Sum();

        return $"{Customer.Name} tegi {Reservations.Length} bronni ja maksis {TotalSum} raha!";
    }

    public string BuildCustomerReport2(Guid CustomerId)
    {
        var Customer = _customerService.GetCustomerAsync(CustomerId).Result;
        var Reservations = _reservationService.GetReservationsForCustomerAsync(CustomerId).Result;
        var EndedReservations = Reservations.Where(IsReservationEnded).ToArray();

        var TotalSum = EndedReservations.Select(GetReservationCost).Sum();

        return $"{Customer.Name} tegi {Reservations.Length} bronni ja maksis {TotalSum} raha!";
    }
    #endregion

    #region kompaktsus
    // 10
    // kasutame palju vahemuutujaid
    // muidu peab neid muutujaid jooksvalt peas tekitama

    public string BuildReservationReport1(Guid customerId)
    {
        var reservations = _reservationService.GetReservationsForCustomerAsync(_customerService.GetCustomerAsync(customerId).Result).Result;
        return $"Klient tegi {reservations.Length} bronni";
    }

    public string BuildReservationReport2(Guid customerId)
    {
        var customer = _customerService.GetCustomerAsync(customerId).Result;
        var reservations = _reservationService.GetReservationsForCustomerAsync(customer).Result;

        return $"Klient tegi {reservations.Length} bronni";
    }
    #endregion

    #region meetodiabstraktsus
    // 11
    // meetodi abstraktsustase peab olema ühtlane
    // ideaalis 1 kiht allapool meetodi nime
    // aju ei taha suure pildi vahel detaile närida

    public void DeactivateInactiveBrooms1()
    {
        var brooms = _reservationService.GetAvailableBroomsAsync().Result;

        foreach (var broom in brooms)
        {
            broom.IsActive = false;
            broom.RegistrationNumber += " - INACTIVE";
            _archiveService.MarkBroomAsDeactivatedAsync(broom).Wait();
            _logger.LogInformation($"Broom {broom.Id} is deactivated");
        }
    }

    public void DeactivateInactiveBrooms2()
    {
        var brooms = _reservationService.GetAvailableBroomsAsync().Result;

        foreach (var broom in brooms)
        {
            DeactivateBroom(broom);
        }
    }

    private void DeactivateBroom(Broom broom)
    {
        broom.IsActive = false;
        broom.RegistrationNumber += " - INACTIVE";
        _archiveService.MarkBroomAsDeactivatedAsync(broom).Wait();
        _logger.LogInformation($"Broom {broom.Id} is deactivated");
    }
    #endregion

    #region debugger
    // 12
    // need logid ei jookse prodis
    // õpi normaalselt debuggerit kasutama

    public decimal CalculateProfitForBroom(Guid broomId, decimal totalSalaryCost)
    {
        _logger.LogDebug($"Start CalculateProfitForBroom");

        var broom = _broomService.GetBroomAsync(broomId).Result;

        var reservations = _reservationService.GetReservationsForBroomAsync(broom).Result;
        var sum = 0m;
        foreach (var reservation in reservations)
        {
            if (IsReservationEnded(reservation))
            {
                _logger.LogDebug($"Adding reservarion {reservation.Id}");
                sum += GetReservationCost(reservation);
            }
        }

        _logger.LogDebug($"End CalculateProfitForBroom");

        return sum;
    }
    #endregion

    #region kahvlid
    // 13
    // koodi loetavus on võrdeline iffide arvuga
    // kahvliga otsused tuleks varakult ja kõrgemal ära teha
    // lipud meetodites on paha-paha
    // tähendab, et meetod teeb mitut asja
    // äri kasvab alati laiali
    // pole mõtet liiga DRY koodi kirjutada

    public void MassActivateCustomers1()
    {
        var customers = _customerService.GetCustomersAsync().Result;

        foreach (var customer in customers)
        {
            var isDangerous = !IsCustomerAdult(customer);
            ActivateCustomer1(customer, isDangerous);
        }
    }

    public void ActivateCustomer1(Customer customer, bool flagAsDangerous)
    {
        customer.IsActive = true;

        if (flagAsDangerous)
        {
            customer.IsDangerous = true;
        }
    }

    // oracle lipu lugu

    public void MassActivateCustomers2()
    {
        var customers = _customerService.GetCustomersAsync().Result;

        foreach (var customer in customers)
        {
            if (IsCustomerAdult(customer))
            {
                ActivateCustomer(customer);
            }
            else
            {
                ActivateDangerousCustomer(customer);
            }
        }
    }

    public void ActivateCustomer(Customer customer)
    {
        customer.IsActive = true;
    }

    public void ActivateDangerousCustomer(Customer customer)
    {
        customer.IsActive = true;
        customer.IsDangerous = true;
    }


    #endregion
}
