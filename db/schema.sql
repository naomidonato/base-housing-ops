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
