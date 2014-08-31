package com.golf1052.quotesapp;

import android.app.Activity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.util.Log;

import com.parse.Parse;
import com.parse.ParseObject;
import com.parse.ParseUser;
import com.parse.ParseException;
import com.parse.SignUpCallback;

import org.json.JSONArray;


public class SignupActivity extends Activity {
    EditText displayNameField;
    EditText emailField;
    EditText passwordField;
    Button accountCreateButton;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_signup);

        displayNameField = (EditText)findViewById(R.id.displayNameField);
        emailField = (EditText)findViewById(R.id.emailField);
        passwordField = (EditText)findViewById(R.id.passwordField);
        accountCreateButton = (Button)findViewById(R.id.accountCreateButton);

        accountCreateButton.setOnClickListener(
                new View.OnClickListener() {
                    @Override
                    public void onClick(View view) {
                        final String displayName = displayNameField.getText().toString();
                        final String email = emailField.getText().toString();
                        final String password = passwordField.getText().toString();

                        createUser(displayName, email, password);
                    }
                }
        );
        // initialize Parse
        Parse.initialize(this, ApiKey.app_id, ApiKey.api_key);


    }

    private void createUser(String displayName, String email, String password) {
        ParseUser user = new ParseUser();
        user.setUsername(email);
        user.setEmail(email);
        user.setPassword(password);

        user.put("displayName", displayName);

        // May not be the correct way to initialize empty relational data array
        user.put("favorites", new JSONArray());

        user.signUpInBackground(new SignUpCallback() {
            public void done(ParseException e) {
                if (e == null) {
                    // Hooray! Let them use the app now.
                    // Start the next Activity'
                    Log.v("SignUpActivity", "Account Created Successfully");
                } else {
                    // Sign up didn't succeed. Look at the ParseException
                    // to figure out what went wrong
                    Log.v("SignUpActivity Errors", e.getMessage());
                }
            }
        });
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.signup, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if (id == R.id.action_settings) {
            return true;
        }
        return super.onOptionsItemSelected(item);
    }
}
