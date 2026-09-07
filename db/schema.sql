CREATE TABLE property (
    property_id SERIAL PRIMARY KEY,
    name        VARCHAR(150) NOT NULL,
    address     VARCHAR(255) NOT NULL,
    created_at  TIMESTAMPTZ NOT NULL DEFAULT now()
);

CREATE TABLE unit (
    unit_id      SERIAL PRIMARY KEY,
    property_id  INTEGER NOT NULL REFERENCES property(property_id),
    unit_number  VARCHAR(20) NOT NULL,
    bedrooms     SMALLINT NOT NULL,
    is_vacant    BOOLEAN NOT NULL DEFAULT true,
    created_at   TIMESTAMPTZ NOT NULL DEFAULT now(),
    UNIQUE (property_id, unit_number)
);

CREATE TABLE resident (
    resident_id       SERIAL PRIMARY KEY,
    first_name        VARCHAR(100) NOT NULL,
    last_name         VARCHAR(100) NOT NULL,
    email             VARCHAR(255) NOT NULL UNIQUE,
    branch_of_service VARCHAR(50),
    created_at        TIMESTAMPTZ NOT NULL DEFAULT now()
);

CREATE TABLE lease (
    lease_id    SERIAL PRIMARY KEY,
    unit_id     INTEGER NOT NULL REFERENCES unit(unit_id),
    start_date  DATE NOT NULL,
    end_date    DATE,
    CHECK (end_date IS NULL OR end_date > start_date)
);

CREATE TABLE lease_resident (
    lease_id     INTEGER NOT NULL REFERENCES lease(lease_id) ON DELETE CASCADE,
    resident_id  INTEGER NOT NULL REFERENCES resident(resident_id) ON DELETE CASCADE,
    role         VARCHAR(20) NOT NULL DEFAULT 'primary'
                 CHECK (role IN ('primary', 'co_signer')),
    PRIMARY KEY (lease_id, resident_id)
);

CREATE TABLE technician (
    technician_id SERIAL PRIMARY KEY,
    name          VARCHAR(150) NOT NULL,
    specialty     VARCHAR(50) NOT NULL
                  CHECK (specialty IN ('plumbing', 'electrical', 'hvac', 'general')),
    created_at    TIMESTAMPTZ NOT NULL DEFAULT now()
);

CREATE TABLE maintenance_request (
    request_id   SERIAL PRIMARY KEY,
    unit_id      INTEGER NOT NULL REFERENCES unit(unit_id),
    resident_id  INTEGER NOT NULL REFERENCES resident(resident_id),
    description  TEXT NOT NULL,
    category      VARCHAR(50),
    urgency       VARCHAR(20)
                  CHECK (urgency IN ('emergency', 'urgent', 'routine')),
    status        VARCHAR(20) NOT NULL DEFAULT 'submitted'
                  CHECK (status IN ('submitted', 'triaged', 'in_progress', 'resolved', 'closed')),
    created_at    TIMESTAMPTZ NOT NULL DEFAULT now()
);

CREATE TABLE work_order (
    work_order_id    SERIAL PRIMARY KEY,
    request_id       INTEGER NOT NULL REFERENCES maintenance_request(request_id),
    technician_id    INTEGER REFERENCES technician(technician_id),
    status           VARCHAR(20) NOT NULL DEFAULT 'unassigned'
                     CHECK (status IN ('unassigned', 'scheduled', 'completed', 'cancelled')),
    scheduled_date   DATE,
    completed_date   DATE,
    created_at       TIMESTAMPTZ NOT NULL DEFAULT now()
);
