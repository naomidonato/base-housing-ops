using BaseHousingOps.Domain.Entities;
using Npgsql;
using NpgsqlTypes;

namespace BaseHousingOps.Infrastructure.Data;

public sealed class MaintenanceRequestRepository
    : IMaintenanceRequestRepository
{
    private readonly NpgsqlDataSource _dataSource;

    public MaintenanceRequestRepository(NpgsqlDataSource dataSource)
    {
        _dataSource = dataSource;
    }

    public async Task<CreatedMaintenanceRequest> CreateAsync(
        MaintenanceRequest request,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            INSERT INTO maintenance_request
                (unit_id, resident_id, description, category, urgency, status)
            VALUES
                ($1, $2, $3, $4, $5, $6)
            RETURNING
                request_id,
                unit_id,
                resident_id,
                description,
                category,
                urgency,
                status;
            """;

        await using var command = _dataSource.CreateCommand(sql);

        command.Parameters.AddWithValue(
            request.UnitId);

        command.Parameters.AddWithValue(
            request.ResidentId);

        command.Parameters.AddWithValue(
            request.Description);

        AddNullableTextParameter(
            command,
            request.Category is null
                ? null
                : request.Category.Value.ToString().ToLowerInvariant());

        AddNullableTextParameter(
            command,
            request.Urgency is null
                ? null
                : request.Urgency.Value.ToString().ToLowerInvariant());

        command.Parameters.AddWithValue(
            request.Status.ToString().ToLowerInvariant());

        await using var reader =
            await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken))
        {
            throw new InvalidOperationException(
                "The database did not return the created maintenance request.");
        }

        return new CreatedMaintenanceRequest(
            Id: reader.GetInt32(0),
            UnitId: reader.GetInt32(1),
            ResidentId: reader.GetInt32(2),
            Description: reader.GetString(3),
            Category: reader.IsDBNull(4)
                ? null
                : reader.GetString(4),
            Urgency: reader.IsDBNull(5)
                ? null
                : reader.GetString(5),
            Status: reader.GetString(6));
    }


        // Add this new method here.
    public async Task<MaintenanceRequestResponse?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        const string sql = """
            SELECT
                request_id,
                unit_id,
                resident_id,
                description,
                category,
                urgency,
                status,
                created_at
            FROM maintenance_request
            WHERE request_id = $1;
            """;

        await using var command = _dataSource.CreateCommand(sql);

        command.Parameters.AddWithValue(id);

        await using var reader =
            await command.ExecuteReaderAsync(cancellationToken);

        if (!await reader.ReadAsync(cancellationToken))
        {
            return null;
        }

        return new MaintenanceRequestResponse(
            Id: reader.GetInt32(0),
            UnitId: reader.GetInt32(1),
            ResidentId: reader.GetInt32(2),
            Description: reader.GetString(3),
            Category: reader.IsDBNull(4)
                ? null
                : reader.GetString(4),
            Urgency: reader.IsDBNull(5)
                ? null
                : reader.GetString(5),
            Status: reader.GetString(6),
            CreatedAt: reader.GetFieldValue<DateTime>(7));
    }



    private static void AddNullableTextParameter(
        NpgsqlCommand command,
        string? value)
    {
        command.Parameters.Add(new NpgsqlParameter
        {
            NpgsqlDbType = NpgsqlDbType.Varchar,
            Value = (object?)value ?? DBNull.Value
        });
    }
}