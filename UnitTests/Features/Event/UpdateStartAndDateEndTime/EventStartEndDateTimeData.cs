namespace UnitTests.Features.Event.UpdateStartAndDateEndTime;

public class EventStartEndDateTimeData
{
    public static IEnumerable<object[]> EventUpdateStartEndTimeData_S1()
    {
        yield return new object[] { new DateTime(2023, 8, 25, 19, 0, 0), new DateTime(2023, 8, 25, 23, 59, 0) };
        yield return new object[] { new DateTime(2023, 8, 25, 12, 0, 0), new DateTime(2023, 8, 25, 16, 30, 0) };
        yield return new object[] { new DateTime(2023, 8, 25, 8, 0, 0), new DateTime(2023, 8, 25, 12, 15, 0) };
        yield return new object[] { new DateTime(2023, 8, 25, 10, 0, 0), new DateTime(2023, 8, 25, 20, 0, 0) };
        yield return new object[] { new DateTime(2023, 8, 25, 13, 0, 0), new DateTime(2023, 8, 25, 23, 0, 0) };
    }

    public static IEnumerable<object[]> EventUpdateStartEndTimeData_S2()
    {
        yield return new object[] { new DateTime(2023, 8, 25, 19, 0, 0), new DateTime(2023, 8, 26, 1, 0, 0) };
        yield return new object[] { new DateTime(2023, 8, 25, 12, 0, 0), new DateTime(2023, 8, 25, 16, 30, 0) };
        yield return new object[] { new DateTime(2023, 8, 25, 8, 0, 0), new DateTime(2023, 8, 25, 12, 15, 0) };
    }

    public static IEnumerable<object[]> EventUpdateStartEndTimeData_F1_StartDateAfterEndDate()
    {
        yield return new object[] { new DateTime(2023, 8, 26, 19, 0, 0), new DateTime(2023, 8, 25, 1, 0, 0) };
        yield return new object[] { new DateTime(2023, 8, 26, 19, 0, 0), new DateTime(2023, 8, 25, 23, 59, 0) };
        yield return new object[] { new DateTime(2023, 8, 27, 12, 0, 0), new DateTime(2023, 8, 25, 16, 30, 0) };
        yield return new object[] { new DateTime(2023, 8, 1, 8, 0, 0), new DateTime(2023, 7, 31, 12, 15, 0) };
    }

    public static IEnumerable<object[]> EventUpdateStartEndTimeData_F2_StartTimeAfterEndTime()
    {
        yield return new object[] { new DateTime(2023, 8, 26, 19, 0, 0), new DateTime(2023, 8, 26, 14, 0, 0) };
        yield return new object[] { new DateTime(2023, 8, 26, 16, 0, 0), new DateTime(2023, 8, 26, 0, 0, 0) };
        yield return new object[] { new DateTime(2023, 8, 26, 19, 0, 0), new DateTime(2023, 8, 26, 18, 59, 0) };
        yield return new object[] { new DateTime(2023, 8, 26, 12, 0, 0), new DateTime(2023, 8, 26, 10, 10, 0) };
        yield return new object[] { new DateTime(2023, 8, 26, 8, 0, 0), new DateTime(2023, 8, 26, 0, 30, 0) };
    }

    public static IEnumerable<object[]> EventUpdateStartEndTimeData_F3_EventDurationTooShort()
    {
        yield return new object[] { new DateTime(2023, 8, 26, 14, 0, 0), new DateTime(2023, 8, 26, 14, 50, 0) };
        yield return new object[] { new DateTime(2023, 8, 26, 18, 0, 0), new DateTime(2023, 8, 26, 18, 59, 0) };
        yield return new object[] { new DateTime(2023, 8, 26, 12, 0, 0), new DateTime(2023, 8, 26, 12, 30, 0) };
        yield return new object[] { new DateTime(2023, 8, 26, 8, 0, 0), new DateTime(2023, 8, 26, 8, 0, 0) };
    }

    public static IEnumerable<object[]> EventUpdateStartEndTimeData_F4_EventDurationTooShort()
    {
        yield return new object[] { new DateTime(2023, 8, 25, 23, 30, 0), new DateTime(2023, 8, 26, 0, 15, 0) };
        yield return new object[] { new DateTime(2023, 8, 30, 23, 1, 0), new DateTime(2023, 8, 31, 0, 0, 0) };
        yield return new object[] { new DateTime(2023, 8, 30, 23, 59, 0), new DateTime(2023, 8, 31, 0, 1, 0) };
    }

    public static IEnumerable<object[]> EventUpdateStartEndTimeData_F5_EventStartTimeToEarly()
    {
        yield return new object[] { new DateTime(2023, 8, 25, 7, 50, 0), new DateTime(2023, 8, 25, 14, 0, 0) };
        yield return new object[] { new DateTime(2023, 8, 25, 7, 59, 0), new DateTime(2023, 8, 25, 15, 0, 0) };
        yield return new object[] { new DateTime(2023, 8, 25, 1, 1, 0), new DateTime(2023, 8, 25, 8, 30, 0) };
        yield return new object[] { new DateTime(2023, 8, 25, 5, 59, 0), new DateTime(2023, 8, 25, 7, 59, 0) };
        yield return new object[] { new DateTime(2023, 8, 25, 0, 59, 0), new DateTime(2023, 8, 25, 7, 59, 0) };
    }

    public static IEnumerable<object[]> EventUpdateStartEndTimeData_F6_EventStartTimeToEarly()
    {
        yield return new object[] { new DateTime(2023, 8, 24, 23, 50, 0), new DateTime(2023, 8, 25, 1, 1, 0) };
        yield return new object[] { new DateTime(2023, 8, 24, 22, 0, 0), new DateTime(2023, 8, 25, 7, 59, 0) };
        yield return new object[] { new DateTime(2023, 8, 30, 23, 0, 0), new DateTime(2023, 8, 31, 2, 30, 0) };
        yield return new object[] { new DateTime(2023, 8, 24, 23, 50, 0), new DateTime(2023, 8, 25, 1, 1, 0) };
    }

    public static IEnumerable<object[]> EventUpdateStartEndTimeData_F9_EventDurationTooLong()
    {
        yield return new object[] { new DateTime(2023, 8, 30, 8, 0, 0), new DateTime(2023, 8, 30, 18, 1, 0) };
        yield return new object[] { new DateTime(2023, 8, 30, 14, 59, 0), new DateTime(2023, 8, 31, 1, 0, 0) };
        yield return new object[] { new DateTime(2023, 8, 30, 14, 0, 0), new DateTime(2023, 8, 31, 0, 1, 0) };
        yield return new object[] { new DateTime(2023, 8, 30, 14, 0, 0), new DateTime(2023, 8, 31, 18, 30, 0) };
    }

    public static IEnumerable<object[]> EventUpdateStartEndTimeData_F11_EventDurationSpansInvalidTime()
    {
        yield return new object[] { new DateTime(2023, 8, 31, 0, 30, 0), new DateTime(2023, 8, 31, 8, 30, 0) };
        yield return new object[] { new DateTime(2023, 8, 30, 23, 59, 0), new DateTime(2023, 8, 31, 8, 1, 0) };
        yield return new object[] { new DateTime(2023, 8, 31, 1, 0, 0), new DateTime(2023, 8, 31, 8, 0, 0) };
    }
}
