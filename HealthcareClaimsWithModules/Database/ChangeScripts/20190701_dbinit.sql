DROP TABLE IF EXISTS batches;

CREATE TABLE batches (
	id UUID PRIMARY KEY,
	batch_uri VARCHAR (2000) NOT NULL,
	submission_feedback_uri VARCHAR (2000) NULL,
	vetting_report_uri VARCHAR (2000) NULL,
	creation_date TIMESTAMP NOT NULL
);


