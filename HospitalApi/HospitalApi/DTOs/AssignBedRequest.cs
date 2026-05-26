namespace HospitalApi.DTOs;

public class AssignBedRequest
{
    public int BedId { get; set; }

    public DateTime From { get; set; }

    public DateTime? To { get; set; }
}