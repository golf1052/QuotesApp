package com.golf1052.quotesapp;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.Window;
import android.widget.Button;

import com.parse.Parse;
import com.parse.ParseAnalytics;


public class MainActivity extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        this.requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.activity_main);
        //initialize Parse
        Parse.initialize(this, ApiKey.app_id, ApiKey.api_key);
        //tracks the app being opened (no intent, and this method does take a null)
        ParseAnalytics.trackAppOpened(null);


           //login and signup buttons and listeners that launch the designated Activities when they are clicked
           final Button login = (Button) findViewById(R.id.login);
           final Button signup = (Button) findViewById(R.id.signup);
           login.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    Intent loginIntent = new Intent(MainActivity.this, LoginActivity.class);
                    MainActivity.this.startActivity(loginIntent);
                }
            });
           signup.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View view) {
                    Intent signupIntent = new Intent(MainActivity.this, SignupActivity.class);
                    MainActivity.this.startActivity(signupIntent);
                }
            });

    }


    /*
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.main, menu);
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
    */
}
